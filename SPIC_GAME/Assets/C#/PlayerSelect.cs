using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerSelect : MonoBehaviour
{
    private CharacterController controller;
    private float horizontalSpeed; //水平移動速度

    [SerializeField]private float MoveSpeed = 10.0f;//移動速度
    [SerializeField]private float maxFallSpeed = 20.0f;//移動速度

    private float moveTimer;//移動時間
    private bool moveCheck;
    private float horiB;//ボタン
    private float direction;
    public int moveCount;
    private int moveCountMax = 3;
    public bool GravityZ;

    [SerializeField]private float gravity = 60.0f;//重力

    private float verticalSpeed; //垂直移動度

    [SerializeField]private float jumpSpeed = 20.0f;//ジャンプ速度
    [SerializeField]private float jumpSpeedForwqrd = 20.0f;//ジャンプ速度
    [SerializeField]private float airControl = 0.3f;//空中入力操作時の補正値
    private int airFrame; //空中にいる時間（フレーム）
    
    private Animator animator;//アニメータのパラメータ

    private Transform activeFloor; //踏んでいる床
    private Vector3 activeLocalFloorPoint; //床を基準にした時の相対位置
    private Vector3 activeGlobalFloorPoint; //世界を基準にした時の相対位置

    private Vector3 orignalP; 
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        moveTimer = 0;
        moveCheck = false;
        horiB = 0;
        moveCount = 1;
        GravityZ = false;
        orignalP = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //重力処理
        UpdateGravity();
        if(GravityZ)
        {
            Debug.Log("aaa");
            float vf = jumpSpeed*40;
            Vector3 move = new Vector3(0, 0, vf);
            controller.Move(move * Time.deltaTime);
            //Debug.Log(move);
            //if (transform.position.y < orignalP.y) GravityZ = false;
        }
        if(!moveCheck)
        {
            horiB = Input.GetAxisRaw("Horizontal");
        }
        if(horiB != 0)
        {
            moveCheck = true;
            if (moveCount <= 1 && horiB < 0) moveCheck = false;
            if (moveCount >= moveCountMax && horiB > 0) moveCheck = false;
            if (horiB > 0 && moveCheck)
            {
                direction = 1;
            }
            if (horiB < 0 && moveCheck)
            { 
                direction = -1;
            }
            
        }
        if(moveCheck)
        {
            //移動更新処理
            UpdateMovement();
            moveTimer += Time.deltaTime;
            if(moveTimer>=1.0f)
            {
                moveTimer = 0;
                moveCheck = false;
                animator.SetFloat("Speed", 0f);
                if (direction > 0) moveCount++;
                if (direction < 0) moveCount--;
            }
        }
        //ジャンプ処理
        UpdateJump();
        //Debug.Log();
    }

    //移動更新処理
    private void UpdateMovement()
    {
        //移動処理(この関数内でOnControllerColliderHit関数が呼ばれる)
        Vector3 move = new Vector3(MoveSpeed * direction, 0, 0);
        Vector3 forward = new Vector3(direction, 0, 0);
        transform.rotation = Quaternion.LookRotation(forward);
        controller.Move(move * Time.deltaTime);
        animator.SetFloat("Speed", 1f);
        //Debug.Log("aaa");
    }
    //重力処理
    private void UpdateGravity()
    {
        //地面に接地しているときの垂直移動速度は一定の重力量
        if (controller.isGrounded)
        {
            verticalSpeed = -gravity * Time.deltaTime;
        }
        //空中では垂直移動速度に重力を加算していく
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
        //垂直移動速度が最大落下速度を超えないように制限
        verticalSpeed = Mathf.Max(verticalSpeed, -maxFallSpeed);
        Vector3 move = new Vector3(0, verticalSpeed, 0);
        controller.Move(move);
    }

    //ジャンプ処理
    private void UpdateJump()
    {
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpSpeed;
                Vector3 forward = new Vector3(direction, 0, 0);
                transform.rotation = Quaternion.LookRotation(forward);
                Vector3 move = new Vector3(0, jumpSpeed, jumpSpeedForwqrd);
                controller.Move(move);
                //アニメータにパラメータを送信
                animator.SetTrigger("Jump");
                GravityZ = true;
            }
        }
        
    }
    //void FixedUpdate()
    //{
    //    //念のためZ軸位置が動かないように固定
    //    transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    //}
    //CharacterControllerの移動でコライダーと衝突した際に呼ばれる
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //天井チェック
        CeilingCheck(hit);
        //床チェック
        FloorCheck(hit);
    }

    //天井チェック
    private void CeilingCheck(ControllerColliderHit hit)
    {
        //当たった面の向きが下方向だったら天井
        if (hit.normal.y < -0.8f)
        {
            //上方向に力が働ている場合はリセット
            if(verticalSpeed>0.0f)
            {
                verticalSpeed = 0.0f;
            }
        }
    }
    //床チェック
    private void FloorCheck(ControllerColliderHit hit)
    {
        //当たった面の向きが上方向だったら床
        if(hit.normal.y>0.9f)
        {
            //踏んでいる床を記憶
            activeFloor = hit.collider.transform;
            //空中時間をリセット
            airFrame = 0;
        }
    }
}

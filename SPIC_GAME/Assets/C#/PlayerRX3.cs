using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerRX3 : MonoBehaviour
{
    private CharacterController controller;
    private float horizontalSpeed; //水平移動速度

    [SerializeField]
    private float maxMoveSpeed = 5.0f;//最大移動速度
    private float RunmaxMoveSpeed = 5.0f;//最大走り移動速度

    [SerializeField]
    private float maxFallSpeed = 20.0f;//最大落下速度

    [SerializeField]
    private float gravity = 60.0f;//重力

    private float verticalSpeed; //垂直移動度

    [SerializeField]
    private float jumpSpeed = 20.0f;//ジャンプ速度

    [SerializeField]
    private float acceleration = 10.0f;//加速度
    private float Runacceleration = 10.0f;//走り加速度

    [SerializeField]
    private float friction = 10.0f;//摩擦係数

    [SerializeField]
    private float airControl = 0.3f;//空中入力操作時の補正値

    private Animator animator;//アニメータのパラメータ

    //[SerializeField]
    //private Text coinText;

    //private int coinCount;//獲得コイン枚数

    //[SerializeField]
    //private Transform spawnPoint;

    //[SerializeField]
    //private Text lifeText;

    //[SerializeField]
    private int life = 1;

    [SerializeField]
    private float pushPower = 0.75f; //押す力

    private Transform activeFloor; //踏んでいる床

    private Vector3 activeLocalFloorPoint; //床を基準にした時の相対位置

    private Vector3 activeGlobalFloorPoint; //世界を基準にした時の相対位置

    private int airFrame; //空中にいる時間（フレーム）

    private int ab = 1;

    private bool run;

    private bool Pupdate;

    private bool isDamaged = false;//敵に当たったかどうか

    public float DamageTime = 0;

    [SerializeField]
    private Material materialss;//ダメージマテリアル

    [SerializeField]
    private Material OrigunslMaterial;//元のマテリアル

    [SerializeField]
    private Material materialMesh;//ヘルメットのマテリアル

    public float playerRotateTime;

    public int cunt = 0;

    [SerializeField]
    private SkinnedMeshRenderer skinnedHelmed;

    public GameObject levelup;
    public GameObject leveldown;
    public static int level;
    public static bool levelupbool;
    [SerializeField] private float airRunSpeed = 2;
    private float airControlRun;

    public static bool jump;
    public static bool jumpG;
    private bool jumpWaza;
    public static float Gmainasu;
    private float air_moveC;//特殊移動
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isDamaged = true;
        Pupdate = true;
        //Spawn();
        ////UI表示更新
        //UpdateCoinText();
        //UpdateLifeText();
        Gmainasu = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(level);
        if (Pupdate)
        {
            //重力処理
            UpdateGravity();

            //アイテム処理
            GetItem();
            //走り移動処理
            UpdateRun();

            //スペシャル
            UpdateSpecial();

            animator.SetBool("Run", run);
            //Debug.Log(horizontalSpeed);
            if (level >= 0)
            {
                //ジャンプ処理
                UpdateJump();

                //進行方向更新処理
                UpdateDirection();


                if (isDamaged)
                {
                    //点滅開始
                    if (cunt < 10)
                    {
                        if (DamageTime < 0.1)
                        {
                            foreach (SkinnedMeshRenderer body in this.GetComponentsInChildren<SkinnedMeshRenderer>())
                            {

                                if (materialss)
                                    body.material = materialss;
                            }
                        }
                        else if (DamageTime >= 0.1)
                        {
                            foreach (SkinnedMeshRenderer body in this.GetComponentsInChildren<SkinnedMeshRenderer>())
                            {

                                if (OrigunslMaterial)
                                    body.material = OrigunslMaterial;
                            }
                            skinnedHelmed.material = materialMesh;
                        }
                        if (DamageTime > 0.2)
                        {
                            DamageTime = 0;
                            cunt++;
                        }
                    }
                    DamageTime += Time.deltaTime;
                    //点滅停止
                    if (cunt >= 10)
                    {
                        foreach (SkinnedMeshRenderer body in this.GetComponentsInChildren<SkinnedMeshRenderer>())
                        {

                            if (OrigunslMaterial)
                                body.material = OrigunslMaterial;
                        }
                        skinnedHelmed.material = materialMesh;
                        cunt = 0;
                        isDamaged = false;
                        DamageTime = 0;

                    }

                }
            }
            else if (level == -1)
            {
                //アニメーターにパラメータを送信
                animator.SetFloat("Speed", 0.0f);
                horizontalSpeed = 0.0f;
                verticalSpeed = 20.0f;

                level--;

            }
            else
            {
                //回転
                if (playerRotateTime < 0.4f)
                {
                    this.transform.Rotate(600 * Time.deltaTime, 0, 0);
                }
                playerRotateTime += Time.deltaTime;
                if (playerRotateTime >= 0.4f)
                {
                    playerRotateTime = 0;
                    cunt++;
                }
            }


            //移動更新処理
            UpdateMovement();
        }
    }

    //進行方向更新処理
    private void UpdateDirection()
    {
        //水平方向入力情報取得
        float horizontal = Input.GetAxisRaw("Horizontal");

        //加速処理
        float power = Mathf.Abs(horizontal);
        if(power>0.1f)
        {
            //水平方向入力情報から進行方向を設定
            Vector3 direction = new Vector3(horizontal, 0, 0);

            //進行方向に向くように回転設定
            transform.rotation = Quaternion.LookRotation(direction);
            float AC = acceleration /*+ Runacceleration*/;
            //加速量を計算
            float speed =  AC * horizontal * Time.deltaTime;
            //反転時だけ移動を早める
            if (Mathf.Sign(horizontalSpeed) * Mathf.Sign(horizontal) < 0) speed *= 5;
            //空中に浮いている時は移動値を補正する
            if (!controller.isGrounded)
            {
                speed *= airControl;
            }

            //加速処理
            horizontalSpeed += speed;

            //速度が一定以上なら制限する
            if(Mathf.Abs(horizontalSpeed)>maxMoveSpeed+RunmaxMoveSpeed)
            {
                horizontalSpeed = Mathf.Sign(horizontalSpeed) * (maxMoveSpeed+RunmaxMoveSpeed);
            }
            
        }
        //減速処理
        else
        {
            if(Mathf.Abs(horizontalSpeed)>0)
            {
                //減速量を計算
                float speed = Mathf.Sign(horizontalSpeed) * friction * Time.deltaTime;

                //空中に浮いている時は移動値を補正する
                if(!controller.isGrounded)
                {
                    speed *= airControl;
                }

                //減速処理
                horizontalSpeed -= speed;

                //速度が一定以下なら止める
                if(Mathf.Abs(horizontalSpeed)<friction*Time.deltaTime)
                {
                    horizontalSpeed = 0.0f;
                }
            }
        }
        //地面に接地している時
        if (controller.isGrounded)
        {
            //アニメーターにパラメータを送信
            animator.SetFloat("Speed", power);
        }
    }

    //移動更新処理
    private void UpdateMovement()
    {
        //アニメータにパラメータを送信
        animator.SetBool("Ground", controller.isGrounded);

        //移動量を計算
        Vector3 move= new Vector3(horizontalSpeed*airControlRun*air_moveC, verticalSpeed, 0);

        //床の移動に追従する処理
        if(activeFloor!=null)
        {
            //前回の床を基準にした位置と今回の床の位置から今回のグローバル基準の位置を算出
            Vector3 newGlobalFloorPoint = activeFloor.TransformPoint(activeLocalFloorPoint);
            //前回と今回のグローバル基準の位置の差分を求めることで床の移動量を算出する
            Vector3 moveDistance = newGlobalFloorPoint - activeGlobalFloorPoint;
            //現在の位置に床の移動量を加算する
            controller.enabled = false;
            transform.position += moveDistance;
            controller.enabled = true;
        }

        //空中時間更新
        airFrame++;
        if(airFrame>2)
        {
            activeFloor = null;
        }

        //移動処理(この関数内でOnControllerColliderHit関数が呼ばれる)
        controller.Move(move * Time.deltaTime);

        //床の移動量算出のため、床の位置情報を記憶しておく
        if(activeFloor!=null)
        {
            //グローバル基準の位置と床を基準のした位置を保存
            activeGlobalFloorPoint = transform.position;
            activeLocalFloorPoint = activeFloor.InverseTransformPoint(transform.position);
        }
    }

    //重力処理
    private void UpdateGravity()
    {
        //地面に接地しているときの垂直移動速度は一定の重力量
        if (controller.isGrounded)
        {
            verticalSpeed = -gravity * Time.deltaTime;
            jump = false;
            jumpG = false;
        }
        //空中では垂直移動速度に重力を加算していく
        else
        {
            if (jumpWaza)
            {
                verticalSpeed = -3;
                jumpWaza = false;
            }
            verticalSpeed -= (gravity - Gmainasu) * Time.deltaTime;
        }
        //垂直移動速度が最大落下速度を超えないように制限
        verticalSpeed = Mathf.Max(verticalSpeed, -maxFallSpeed);
    }

    //ジャンプ処理
    private void UpdateJump()
    {
        //地面に接地している状態でジャンプボタンを押すと垂直移動速度を設定する
        if(controller.isGrounded)
        {
            if(Input.GetButtonDown("Jump")&&!jumpG)
            {
                verticalSpeed = jumpSpeed;
                jump = true;
                //アニメータにパラメータを送信
                animator.SetTrigger("Jump");
            }
        }
    }
    private void UpdateRun()
    {
        //地上走り移動処理
        if (Input.GetKey(KeyCode.N)/*GetKeyDown(KeyCode.N)*/&& controller.isGrounded)
        {
            RunmaxMoveSpeed = maxMoveSpeed * 2;
            Runacceleration = acceleration;
            run = true;
        }
        else
        {
            RunmaxMoveSpeed = 0;
            Runacceleration = 0;
            run = false;
        }
        if (Input.GetKey(KeyCode.N)/*GetKeyDown(KeyCode.N)*/&& !controller.isGrounded)
        {
            airControlRun = airRunSpeed;
            Debug.Log("aaa");
        }
        else
        {
            airControlRun = 1;
        }
    }

    private void UpdateSpecial()
    {
        if (jumpG && jump && verticalSpeed < -2)
        {
            if (Input.GetButton("Jump"))
            {
                if (Input.GetButtonDown("Jump")) jumpWaza = true;
                Gmainasu = 59;
                air_moveC = 5;
            }
            else
            {
                Gmainasu = 0;
                air_moveC = 1;
            }
        }
        if (controller.isGrounded||jumpG)
        {
            Gmainasu = 0;
            air_moveC = 1;
        }
    }

    //コインUI表示更新
    //private void UpdateCoinText()
    //{
    //    coinText.text = "x" + coinCount;
    //}
    ////コイン獲得処理
    //public void AddCoin(int amount)
    //{
    //    coinCount += amount;

    //    //UI表示更新
    //    UpdateCoinText();
    //}

    //固定時間毎更新処理
    void FixedUpdate()
    {
        //念のためZ軸位置が動かないように固定
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    //スポーン
    //private void Spawn()
    //{
    //    //移動力リセット
    //    verticalSpeed = 0.0f;
    //    horizontalSpeed = 0.0f;


    //    Warp(spawnPoint.position);
    //}

    //ワープ
    public void Warp(Vector3 position)
    {
        //※キャラクターコントローラー使用時に直接位置を変更する場合があは
        //　キャラクターコントローラーを無効化してから設定する必要がある
        controller.enabled = false;
        transform.position = position;
        controller.enabled = true;
    }

    //死亡処理
    public void Death()
    {
        if (!isDamaged)
        {
            //life -= 1;
            level -= 1;
            //別で使う      
            if (level < 0)
            { 
                isDamaged = true;
            }
            else
            {
                Destroy(this.gameObject);
                Instantiate(leveldown, this.transform.position, this.transform.rotation);
                level -= 1;
            }
        }
        //ライフを減らす
        //life--;
        //if (life <= 0)
        //{
        //    //現在のシーンを再ロード
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
        ////UI表示更新
        //UpdateLifeText();
        ////スポーン
        //Spawn();
    }

    ////ライフUI表示更新
    //private void UpdateLifeText()
    //{
    //    lifeText.text = "x" + life;
    //}

    ////スポーンポイント設定
    //public void SetSpawnPoint(Transform transform)
    //{
    //    spawnPoint = transform;
    //}

    //CharacterControllerの移動でコライダーと衝突した際に呼ばれる
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //剛体を押す
        PushRigidbody(hit);

        //天井チェック
        CeilingCheck(hit);

        //デストリガーとの衝突処理
        CollisionDeathTrigger(hit);

        //床チェック
        FloorCheck(hit);

        if(life<0)
        {
            Destroy(hit.collider);
        }
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

    //接触した剛体を押す
    private void PushRigidbody(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        //リジッドディチェック
        if(body==null||body.isKinematic)
        {
            return;
        }
        //下向きには押さない
        if(hit.moveDirection.y<-0.3f)
        {
            return;
        }
        //移動方向から押す方向を算出する(上下方向には押さない)
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        Vector3 pushVelocity = pushDir * pushPower;
        pushVelocity.y = body.velocity.y;
        //押す力を設定
        body.velocity = pushVelocity;
    }

    //デストリガーとの衝突処理
    private void CollisionDeathTrigger(ControllerColliderHit hit)
    {
        //デストリガーと衝突したら死亡する
        DeathTrigger deathTrigger = hit.gameObject.GetComponent<DeathTrigger>();
        if (deathTrigger != null)
        {
            //点滅が終わっているか
            if (!isDamaged)
            {
                Death();
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

    //回転しているか判断
    public void lnversionbool()
    {
        if (Pupdate)
        {
            Pupdate = false;
        }
        else
        {
            Pupdate = true;
        }
    }
    //アイテムゲット
    private void GetItem()
    {
        if(levelupbool)
        {
            level++;
            Instantiate(levelup, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
    public bool PlayerUpdateBool()
    {
        return Pupdate;
    }

    public int plLife()
    {
        return life;
    }
}

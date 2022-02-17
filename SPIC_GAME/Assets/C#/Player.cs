using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxMoveS = 10.0f;
    private CharacterController controller;
    private float horiSpeed;

    private float maxFallS = 15.0f;
    private float G = 60.0f;
    private float verticalS;

    private float kasoku = 10f;
    private float masatsu = 10f;
    private float airControl = 0.3f;

    private float jumpS = 25.0f;
    private int JCount;
    private int Jump = 1;

    private Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anime = GetComponent<Animator>();
        JCount = Jump;
    }

    // Update is called once per frame
    void Update()
    {
        //手前みそ
        //float Hori = Input.GetAxisRaw("Horizontal");

        //Vector3 move;
        //move.x = Hori * maxMoveS;
        //move.y = 0;
        //move.z = 0;
        //controller.Move(move * Time.deltaTime);

        UpdateDirection();
        UpdateMovement();
        UpdateGravity();
        UpdateJump();
    }

    private void UpdateDirection()
    {
        float Hori = Input.GetAxisRaw("Horizontal");

        float power = Mathf.Abs(Hori);
        if (power > 0.1f)
        {
            Vector3 direc = new Vector3(Hori, 0, 0);
            transform.rotation = Quaternion.LookRotation(direc);


            //加速計算
            float S = Hori * kasoku * Time.deltaTime;
              //反対向き減速計算
            if (Mathf.Sign(horiSpeed)*Mathf.Sign(Hori) < 0) S *= 3;
            
            if (!controller.isGrounded)
            {
                S *= airControl;
            }
            
            horiSpeed += S;
            if (Mathf.Abs(horiSpeed) > maxMoveS)
            {
                horiSpeed = Mathf.Sign(horiSpeed) * maxMoveS;
            }
        }
        else
        {
            //減速計算
            if (Mathf.Abs(horiSpeed) > 0)
            {
                float S = Mathf.Sign(horiSpeed) * masatsu * Time.deltaTime;
                if (!controller.isGrounded)
                {
                    S *= airControl;
                }
                horiSpeed -= S;
                if (Mathf.Abs(horiSpeed) < masatsu * Time.deltaTime)
                {
                    horiSpeed = 0.0f;
                }
            }
        }
        //horiSpeed = Hori * maxMoveS;
        //アニメ
        if (controller.isGrounded)
        {
            anime.SetFloat("Speed", power);
        }
        //Debug.Log(horiSpeed);
    }
    private void UpdateGravity()
    {
        if (controller.isGrounded)
        {
            verticalS = -G * Time.deltaTime;
        }
        else
        {
            verticalS -= G * Time.deltaTime;
        }
        verticalS = Mathf.Max(verticalS, -maxFallS);
    }
    private void UpdateJump()
    {
        if (controller.isGrounded)
        {
            JCount = Jump;
        }
        //if(controller.isGrounded)
        if (JCount > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                verticalS += jumpS;
                anime.SetTrigger("Jump");
                JCount--;
            }
        }
    }
    private void UpdateMovement()
    {
        anime.SetBool("Ground", controller.isGrounded);
        Vector3 move = new Vector3(horiSpeed, verticalS, 0);
        controller.Move(move * Time.deltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //コライダーとの衝突時に呼ぶ
        CeilingCheck(hit);
    }
    private void CeilingCheck(ControllerColliderHit hit)
    {
        if (hit.normal.y < -0.8f)
        {
            if (verticalS > 0.0f)
            {
                verticalS = 0;
            }
        }
    }
}
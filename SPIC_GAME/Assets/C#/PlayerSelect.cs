using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerSelect : MonoBehaviour
{
    private CharacterController controller;
    private float horizontalSpeed; //�����ړ����x

    [SerializeField]private float MoveSpeed = 10.0f;//�ړ����x
    [SerializeField]private float maxFallSpeed = 20.0f;//�ړ����x

    private float moveTimer;//�ړ�����
    private bool moveCheck;
    private float horiB;//�{�^��
    private float direction;
    public int moveCount;
    private int moveCountMax = 3;
    public bool GravityZ;

    [SerializeField]private float gravity = 60.0f;//�d��

    private float verticalSpeed; //�����ړ��x

    [SerializeField]private float jumpSpeed = 20.0f;//�W�����v���x
    [SerializeField]private float jumpSpeedForwqrd = 20.0f;//�W�����v���x
    [SerializeField]private float airControl = 0.3f;//�󒆓��͑��쎞�̕␳�l
    private int airFrame; //�󒆂ɂ��鎞�ԁi�t���[���j
    
    private Animator animator;//�A�j���[�^�̃p�����[�^

    private Transform activeFloor; //����ł��鏰
    private Vector3 activeLocalFloorPoint; //������ɂ������̑��Έʒu
    private Vector3 activeGlobalFloorPoint; //���E����ɂ������̑��Έʒu

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

        //�d�͏���
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
            //�ړ��X�V����
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
        //�W�����v����
        UpdateJump();
        //Debug.Log();
    }

    //�ړ��X�V����
    private void UpdateMovement()
    {
        //�ړ�����(���̊֐�����OnControllerColliderHit�֐����Ă΂��)
        Vector3 move = new Vector3(MoveSpeed * direction, 0, 0);
        Vector3 forward = new Vector3(direction, 0, 0);
        transform.rotation = Quaternion.LookRotation(forward);
        controller.Move(move * Time.deltaTime);
        animator.SetFloat("Speed", 1f);
        //Debug.Log("aaa");
    }
    //�d�͏���
    private void UpdateGravity()
    {
        //�n�ʂɐڒn���Ă���Ƃ��̐����ړ����x�͈��̏d�͗�
        if (controller.isGrounded)
        {
            verticalSpeed = -gravity * Time.deltaTime;
        }
        //�󒆂ł͐����ړ����x�ɏd�͂����Z���Ă���
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
        //�����ړ����x���ő嗎�����x�𒴂��Ȃ��悤�ɐ���
        verticalSpeed = Mathf.Max(verticalSpeed, -maxFallSpeed);
        Vector3 move = new Vector3(0, verticalSpeed, 0);
        controller.Move(move);
    }

    //�W�����v����
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
                //�A�j���[�^�Ƀp�����[�^�𑗐M
                animator.SetTrigger("Jump");
                GravityZ = true;
            }
        }
        
    }
    //void FixedUpdate()
    //{
    //    //�O�̂���Z���ʒu�������Ȃ��悤�ɌŒ�
    //    transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    //}
    //CharacterController�̈ړ��ŃR���C�_�[�ƏՓ˂����ۂɌĂ΂��
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //�V��`�F�b�N
        CeilingCheck(hit);
        //���`�F�b�N
        FloorCheck(hit);
    }

    //�V��`�F�b�N
    private void CeilingCheck(ControllerColliderHit hit)
    {
        //���������ʂ̌�������������������V��
        if (hit.normal.y < -0.8f)
        {
            //������ɗ͂����Ă���ꍇ�̓��Z�b�g
            if(verticalSpeed>0.0f)
            {
                verticalSpeed = 0.0f;
            }
        }
    }
    //���`�F�b�N
    private void FloorCheck(ControllerColliderHit hit)
    {
        //���������ʂ̌�����������������珰
        if(hit.normal.y>0.9f)
        {
            //����ł��鏰���L��
            activeFloor = hit.collider.transform;
            //�󒆎��Ԃ����Z�b�g
            airFrame = 0;
        }
    }
}

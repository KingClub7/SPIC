using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerRX3 : MonoBehaviour
{
    private CharacterController controller;
    private float horizontalSpeed; //�����ړ����x

    [SerializeField]
    private float maxMoveSpeed = 5.0f;//�ő�ړ����x
    private float RunmaxMoveSpeed = 5.0f;//�ő呖��ړ����x

    [SerializeField]
    private float maxFallSpeed = 20.0f;//�ő嗎�����x

    [SerializeField]
    private float gravity = 60.0f;//�d��

    private float verticalSpeed; //�����ړ��x

    [SerializeField]
    private float jumpSpeed = 20.0f;//�W�����v���x

    [SerializeField]
    private float acceleration = 10.0f;//�����x
    private float Runacceleration = 10.0f;//��������x

    [SerializeField]
    private float friction = 10.0f;//���C�W��

    [SerializeField]
    private float airControl = 0.3f;//�󒆓��͑��쎞�̕␳�l

    private Animator animator;//�A�j���[�^�̃p�����[�^

    //[SerializeField]
    //private Text coinText;

    //private int coinCount;//�l���R�C������

    //[SerializeField]
    //private Transform spawnPoint;

    //[SerializeField]
    //private Text lifeText;

    //[SerializeField]
    private int life = 1;

    [SerializeField]
    private float pushPower = 0.75f; //������

    private Transform activeFloor; //����ł��鏰

    private Vector3 activeLocalFloorPoint; //������ɂ������̑��Έʒu

    private Vector3 activeGlobalFloorPoint; //���E����ɂ������̑��Έʒu

    private int airFrame; //�󒆂ɂ��鎞�ԁi�t���[���j

    private int ab = 1;

    private bool run;

    private bool Pupdate;

    private bool isDamaged = false;//�G�ɓ����������ǂ���

    public float DamageTime = 0;

    [SerializeField]
    private Material materialss;//�_���[�W�}�e���A��

    [SerializeField]
    private Material OrigunslMaterial;//���̃}�e���A��

    [SerializeField]
    private Material materialMesh;//�w�����b�g�̃}�e���A��

    public float playerRotateTime;

    public int cunt = 0;

    [SerializeField]
    private SkinnedMeshRenderer skinnedHelmed;

    public GameObject levelup;
    public GameObject leveldown;
    public static int level;
    public static bool levelupbool;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isDamaged = true;
        Pupdate = true;
        //Spawn();
        ////UI�\���X�V
        //UpdateCoinText();
        //UpdateLifeText();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(level);
        if (Pupdate)
        {
            //�d�͏���
            UpdateGravity();

            //�A�C�e������
            GetItem();

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

            animator.SetBool("Run", run);
            //Debug.Log(horizontalSpeed);
            if (level >= 0)
            {
                //�W�����v����
                UpdateJump();

                //�i�s�����X�V����
                UpdateDirection();


                if (isDamaged)
                {
                    //�_�ŊJ�n
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
                    //�_�Œ�~
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
                //�A�j���[�^�[�Ƀp�����[�^�𑗐M
                animator.SetFloat("Speed", 0.0f);
                horizontalSpeed = 0.0f;
                verticalSpeed = 20.0f;

                level--;

            }
            else
            {
                //��]
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


            //�ړ��X�V����
            UpdateMovement();
        }
    }

    //�i�s�����X�V����
    private void UpdateDirection()
    {
        //�����������͏��擾
        float horizontal = Input.GetAxisRaw("Horizontal");

        //��������
        float power = Mathf.Abs(horizontal);
        if(power>0.1f)
        {
            //�����������͏�񂩂�i�s������ݒ�
            Vector3 direction = new Vector3(horizontal, 0, 0);

            //�i�s�����Ɍ����悤�ɉ�]�ݒ�
            transform.rotation = Quaternion.LookRotation(direction);
            float AC = acceleration /*+ Runacceleration*/;
            //�����ʂ��v�Z
            float speed =  AC * horizontal * Time.deltaTime;
            //���]�������ړ��𑁂߂�
            if (Mathf.Sign(horizontalSpeed) * Mathf.Sign(horizontal) < 0) speed *= 5;
            //�󒆂ɕ����Ă��鎞�͈ړ��l��␳����
            if (!controller.isGrounded)
            {
                speed *= airControl;
            }

            //��������
            horizontalSpeed += speed;

            //���x�����ȏ�Ȃ琧������
            if(Mathf.Abs(horizontalSpeed)>maxMoveSpeed+RunmaxMoveSpeed)
            {
                horizontalSpeed = Mathf.Sign(horizontalSpeed) * (maxMoveSpeed+RunmaxMoveSpeed);
            }
            
        }
        //��������
        else
        {
            if(Mathf.Abs(horizontalSpeed)>0)
            {
                //�����ʂ��v�Z
                float speed = Mathf.Sign(horizontalSpeed) * friction * Time.deltaTime;

                //�󒆂ɕ����Ă��鎞�͈ړ��l��␳����
                if(!controller.isGrounded)
                {
                    speed *= airControl;
                }

                //��������
                horizontalSpeed -= speed;

                //���x�����ȉ��Ȃ�~�߂�
                if(Mathf.Abs(horizontalSpeed)<friction*Time.deltaTime)
                {
                    horizontalSpeed = 0.0f;
                }
            }
        }
        //�n�ʂɐڒn���Ă��鎞
        if (controller.isGrounded)
        {
            //�A�j���[�^�[�Ƀp�����[�^�𑗐M
            animator.SetFloat("Speed", power);
        }
    }

    //�ړ��X�V����
    private void UpdateMovement()
    {
        //�A�j���[�^�Ƀp�����[�^�𑗐M
        animator.SetBool("Ground", controller.isGrounded);

        //�ړ��ʂ��v�Z
        Vector3 move= new Vector3(horizontalSpeed, verticalSpeed, 0);

        //���̈ړ��ɒǏ]���鏈��
        if(activeFloor!=null)
        {
            //�O��̏�����ɂ����ʒu�ƍ���̏��̈ʒu���獡��̃O���[�o����̈ʒu���Z�o
            Vector3 newGlobalFloorPoint = activeFloor.TransformPoint(activeLocalFloorPoint);
            //�O��ƍ���̃O���[�o����̈ʒu�̍��������߂邱�Ƃŏ��̈ړ��ʂ��Z�o����
            Vector3 moveDistance = newGlobalFloorPoint - activeGlobalFloorPoint;
            //���݂̈ʒu�ɏ��̈ړ��ʂ����Z����
            controller.enabled = false;
            transform.position += moveDistance;
            controller.enabled = true;
        }

        //�󒆎��ԍX�V
        airFrame++;
        if(airFrame>2)
        {
            activeFloor = null;
        }

        //�ړ�����(���̊֐�����OnControllerColliderHit�֐����Ă΂��)
        controller.Move(move * Time.deltaTime);

        //���̈ړ��ʎZ�o�̂��߁A���̈ʒu�����L�����Ă���
        if(activeFloor!=null)
        {
            //�O���[�o����̈ʒu�Ə�����̂����ʒu��ۑ�
            activeGlobalFloorPoint = transform.position;
            activeLocalFloorPoint = activeFloor.InverseTransformPoint(transform.position);
        }
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
    }

    //�W�����v����
    private void UpdateJump()
    {
        //�n�ʂɐڒn���Ă����ԂŃW�����v�{�^���������Ɛ����ړ����x��ݒ肷��
        if(controller.isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpSpeed;
                //�A�j���[�^�Ƀp�����[�^�𑗐M
                animator.SetTrigger("Jump");
            }
        }
    }

    //�R�C��UI�\���X�V
    //private void UpdateCoinText()
    //{
    //    coinText.text = "x" + coinCount;
    //}
    ////�R�C���l������
    //public void AddCoin(int amount)
    //{
    //    coinCount += amount;

    //    //UI�\���X�V
    //    UpdateCoinText();
    //}

    //�Œ莞�Ԗ��X�V����
    void FixedUpdate()
    {
        //�O�̂���Z���ʒu�������Ȃ��悤�ɌŒ�
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    //�X�|�[��
    //private void Spawn()
    //{
    //    //�ړ��̓��Z�b�g
    //    verticalSpeed = 0.0f;
    //    horizontalSpeed = 0.0f;


    //    Warp(spawnPoint.position);
    //}

    //���[�v
    public void Warp(Vector3 position)
    {
        //���L�����N�^�[�R���g���[���[�g�p���ɒ��ڈʒu��ύX����ꍇ������
        //�@�L�����N�^�[�R���g���[���[�𖳌������Ă���ݒ肷��K�v������
        controller.enabled = false;
        transform.position = position;
        controller.enabled = true;
    }

    //���S����
    public void Death()
    {
        if (!isDamaged)
        {
            //life -= 1;
            level -= 1;
            //�ʂŎg��      
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
        //���C�t�����炷
        //life--;
        //if (life <= 0)
        //{
        //    //���݂̃V�[�����ă��[�h
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
        ////UI�\���X�V
        //UpdateLifeText();
        ////�X�|�[��
        //Spawn();
    }

    ////���C�tUI�\���X�V
    //private void UpdateLifeText()
    //{
    //    lifeText.text = "x" + life;
    //}

    ////�X�|�[���|�C���g�ݒ�
    //public void SetSpawnPoint(Transform transform)
    //{
    //    spawnPoint = transform;
    //}

    //CharacterController�̈ړ��ŃR���C�_�[�ƏՓ˂����ۂɌĂ΂��
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //���̂�����
        PushRigidbody(hit);

        //�V��`�F�b�N
        CeilingCheck(hit);

        //�f�X�g���K�[�Ƃ̏Փˏ���
        CollisionDeathTrigger(hit);

        //���`�F�b�N
        FloorCheck(hit);

        if(life<0)
        {
            Destroy(hit.collider);
        }
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

    //�ڐG�������̂�����
    private void PushRigidbody(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        //���W�b�h�f�B�`�F�b�N
        if(body==null||body.isKinematic)
        {
            return;
        }
        //�������ɂ͉����Ȃ�
        if(hit.moveDirection.y<-0.3f)
        {
            return;
        }
        //�ړ��������牟���������Z�o����(�㉺�����ɂ͉����Ȃ�)
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        Vector3 pushVelocity = pushDir * pushPower;
        pushVelocity.y = body.velocity.y;
        //�����͂�ݒ�
        body.velocity = pushVelocity;
    }

    //�f�X�g���K�[�Ƃ̏Փˏ���
    private void CollisionDeathTrigger(ControllerColliderHit hit)
    {
        //�f�X�g���K�[�ƏՓ˂����玀�S����
        DeathTrigger deathTrigger = hit.gameObject.GetComponent<DeathTrigger>();
        if (deathTrigger != null)
        {
            //�_�ł��I����Ă��邩
            if (!isDamaged)
            {
                Death();
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

    //��]���Ă��邩���f
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
    //�A�C�e���Q�b�g
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
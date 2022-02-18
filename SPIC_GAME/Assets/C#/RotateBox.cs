using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBox : MonoBehaviour
{
    //[SerializeField]
    //private int itemCount = 5;

    //[SerializeField]
    //private GameObject coinPrefab;

    //[SerializeField]
    //private Transform appearPoint;

    [SerializeField]
    private float pushPower = 0.1f;//������������

    [SerializeField]
    private float gravity = 0.5f;//�d��

    private Vector3 originalPosition;//�����ʒu

    private Vector3 position;//���݈ʒu

    

    private bool isMoving;//�����Ă����Ԃ�

    private float verticalSpeed;//�����ړ����x

    [SerializeField]
    private Transform target;//��]�����̈ʒu

    [SerializeField]
    private Transform target2;//�ǂꂾ����]���邩


    [SerializeField]
    private float smoothness = 10.0f;

    [SerializeField]
    private float Boxtime;

    private bool floorR=false;

    //[SerializeField]
    //private Material emptyBoxMaterial;//�}�e���A��

    // Start is called before the first frame update
    void Start()
    {
        position = originalPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //�����h��鏈��
        if (isMoving)
        {
            verticalSpeed -= gravity * Time.deltaTime;
            position.y += verticalSpeed;
            if (position.y < originalPosition.y)
            {
                position.y = originalPosition.y;
                isMoving = false;
            }
            transform.position = position;
        }

        if (floorR)
        {
            if(Boxtime>1.0f)
            target.Rotate(0, 0, 180 * Time.deltaTime);
        }
        Boxtime += Time.deltaTime;
    }
    //���̉����ɓ����������̏���
    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[�ɏՓ˂����ꍇ
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            

                //���h��̏����J�n
                verticalSpeed = pushPower;
                isMoving = true;

            if (target != null)
            {
                //�ڕW�n�_�֕⊮���Ȃ���ړ�
                target.Rotate(0, 0, 180);
                //target.rotation = Quaternion.Lerp(target.rotation, target2.rotation, smoothness);
                //target.position = Vector3.Lerp(transform.position, target.position, smoothness);
            }
            //if (floorRotate)
            //{
            //    //��]
            //    if (rotateTime < 0.5f)
            //    {
            //        movefloorT.Rotate(180 * Time.deltaTime, 0, 0);
            //    }
            //    rotateTime += Time.deltaTime;
            //}
            //if (rotateTime >= 0.5f)
            //{
            //    floorRotate = false;
            //    rotateTime = 0;
            //}

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBox1 : MonoBehaviour
{
    //[SerializeField]
    //private int itemCount = 5;

    //[SerializeField]
    //private GameObject coinPrefab;

    //[SerializeField]
    //private Transform appearPoint;



    private Vector3 originalPosition;//�����ʒu

    private Vector3 position;//���݈ʒu

    

    private bool isMoving;//�����Ă����Ԃ�

    private float verticalSpeed;//�����ړ����x

    [SerializeField]
    private Transform oyaPos;//�e�̍��W




    [SerializeField]
    private float smoothness = 10.0f;

    [SerializeField]
    private float Boxtime;

    private bool floorR=false;

    [SerializeField]
    private float speed = 120;

    private Player player;

    private bool upRotateBox;

    private int cuntr;
    //[SerializeField]
    //private Material emptyBoxMaterial;//�}�e���A��

    // Start is called before the first frame update
    void Start()
    {
        Boxtime =0;
        position = originalPosition = transform.position;
        cuntr = 1;
    }
    

    // Update is called once per frame
    void Update()
    {
        


        if (floorR)
        {
            Boxtime += Time.deltaTime;
            if (Boxtime < 1.6f)
            {
                // oyaPos.Rotate(0, 0, 90f * Time.deltaTime);
                float sp = speed * Time.deltaTime;
                oyaPos.rotation = Quaternion.RotateTowards(oyaPos.rotation, Quaternion.Euler(0, 0, -180f * cuntr), sp);
            }
        }

        if (Boxtime >= 1.6f)
        {
            floorR = false;
            Boxtime = 0;
            player.lnversionbool();
            player = null;
            upRotateBox = false;
            cuntr++;
        }
    }
    //���̉����ɓ����������̏���
    private void OnTriggerEnter(Collider other)
    {
        if (upRotateBox == false)
        {
            //�v���C���[�ɏՓ˂����ꍇ
            player = other.GetComponent<Player>();
            PlayerRX player2 = other.GetComponent<PlayerRX>();
            PlayerRX2 player3 = other.GetComponent<PlayerRX2>();
            PlayerRX3 player4 = other.GetComponent<PlayerRX3>();
            if (player != null || player2 != null || player3 != null || player4 != null)
            {





                if (oyaPos != null)
                {
                    floorR = true;
                    foreach (BoxCollider body in this.GetComponentsInChildren<BoxCollider>())
                    {

                        if (body)
                        {
                            if (body.isTrigger)
                            {
                                body.center = -body.center;
                            }
                        }

                    }

                    player.lnversionbool();

                    upRotateBox = true;

                }


            }
        }
    }


}

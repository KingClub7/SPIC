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

    private Player player;

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
        


        if (floorR)
        {
            if (Boxtime < 1.0f)
            {
                oyaPos.Rotate(0, 0, 180 * Time.deltaTime);
            }
            Boxtime += Time.deltaTime;
        }
       
        if (Boxtime >= 1.0f)
        {
            floorR = false;
            Boxtime = 0;
            player.lnversionbool();
            player = null;
        }
    }
    //���̉����ɓ����������̏���
    private void OnTriggerEnter(Collider other)
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
                player.lnversionbool();

            }


        }
    }


}

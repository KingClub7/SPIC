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
    private Transform target;//��]�����̈ʒu




    [SerializeField]
    private float smoothness = 10.0f;

    [SerializeField]
    private float Boxtime;

    private bool floorR=false;

    [SerializeField]
    private Player pl;

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
                target.Rotate(0, 0, 90 * Time.deltaTime);
            }
            Boxtime += Time.deltaTime;
        }
       
        if (Boxtime >= 1.0f)
        {
            floorR = false;
            Boxtime = 0;
            pl.lnversionbool();
        }
    }
    //���̉����ɓ����������̏���
    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[�ɏՓ˂����ꍇ
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            




            if (target != null)
            {
                floorR = true;
                pl.lnversionbool();

            }


        }
    }


}
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



    private Vector3 originalPosition;//初期位置

    private Vector3 position;//現在位置

    

    private bool isMoving;//動いている状態か

    private float verticalSpeed;//垂直移動速度

    [SerializeField]
    private Transform oyaPos;//親の座標




    [SerializeField]
    private float smoothness = 10.0f;

    [SerializeField]
    private float Boxtime;

    private bool floorR=false;

    [SerializeField]
    private float speed = 120;

    private Player player;
    private PlayerRX player2;
    private PlayerRX2 player3;
    private PlayerRX3 player4;

    private bool upRotateBox;

    private int Pcase;

    private int cuntr;
    //[SerializeField]
    //private Material emptyBoxMaterial;//マテリアル

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
            switch(Pcase)
            {
                case 1:
                    player.lnversionbool();
                    player = null;
                    break;
                case 2:
                    player2.lnversionbool();
                    player2 = null;
                    break;
                case 3:
                    player3.lnversionbool();
                    player3 = null;
                    break;
                case 4:
                    player4.lnversionbool();
                    player4 = null;
                    break;
            }
           
            upRotateBox = false;
            cuntr++;
        }
    }
    //箱の下側に当たった時の処理
    private void OnTriggerEnter(Collider other)
    {
        if (upRotateBox == false)
        {
            //プレイヤーに衝突した場合
            player = other.GetComponent<Player>();
            player2 = other.GetComponent<PlayerRX>();
            player3 = other.GetComponent<PlayerRX2>();
            player4 = other.GetComponent<PlayerRX3>();
            if (player != null)
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
                                Pcase = 1;
                                body.center = -body.center;
                            }
                        }
                    }
                    player.lnversionbool();
                    upRotateBox = true;
                }
            }
            if (player2 != null)
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
                                Pcase = 2;
                                body.center = -body.center;
                            }
                        }
                    }
                    player2.lnversionbool();
                    upRotateBox = true;
                }
            }
            if (player3 != null)
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
                                Pcase = 3;
                                body.center = -body.center;
                            }
                        }
                    }
                    player3.lnversionbool();
                    upRotateBox = true;
                }
            }
            if (player4 != null)
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
                                Pcase = 4;
                                body.center = -body.center;
                            }
                        }
                    }
                    player4.lnversionbool();
                    upRotateBox = true;
                }
            }
        }
    }


}

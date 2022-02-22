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
    private Transform target;//親の座標




    [SerializeField]
    private float smoothness = 10.0f;

    [SerializeField]
    private float Boxtime;

    private bool floorR=false;

    private Player player;

    //[SerializeField]
    //private Material emptyBoxMaterial;//マテリアル

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
                target.Rotate(0, 0, 180 * Time.deltaTime);
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
    //箱の下側に当たった時の処理
    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーに衝突した場合
        player = other.GetComponent<Player>();
        if (player != null)
        {
            




            if (target != null)
            {
                floorR = true;
                player.lnversionbool();

            }


        }
    }


}

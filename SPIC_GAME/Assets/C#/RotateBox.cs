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
    private float pushPower = 0.1f;//箱をたたく力

    [SerializeField]
    private float gravity = 0.5f;//重力

    private Vector3 originalPosition;//初期位置

    private Vector3 position;//現在位置

    

    private bool isMoving;//動いている状態か

    private float verticalSpeed;//垂直移動速度

    [SerializeField]
    private Transform target;//回転する基準の位置

    [SerializeField]
    private Transform target2;//どれだけ回転するか


    [SerializeField]
    private float smoothness = 10.0f;

    [SerializeField]
    private float Boxtime;

    private bool floorR=false;

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
        
        //箱が揺れる処理
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
    //箱の下側に当たった時の処理
    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーに衝突した場合
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            

                //箱揺れの処理開始
                verticalSpeed = pushPower;
                isMoving = true;

            if (target != null)
            {
                //目標地点へ補完しながら移動
                target.Rotate(0, 0, 180);
                //target.rotation = Quaternion.Lerp(target.rotation, target2.rotation, smoothness);
                //target.position = Vector3.Lerp(transform.position, target.position, smoothness);
            }
            //if (floorRotate)
            //{
            //    //回転
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

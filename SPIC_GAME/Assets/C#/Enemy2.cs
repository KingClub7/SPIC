using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField]
    private Vector3 move = new Vector3(-2f, 0, 0);
    [SerializeField]
    private float time = 1f;

    private Vector3 originalPosition;
    private float theta;
    [SerializeField]
    private bool floorMove;
    private bool floorRotate;

    [SerializeField]
    private Transform movefloorT;

    //[SerializeField]
    //private GameObject buttonRotate;

    private float rotateTime;



    [SerializeField]
    private Collider collide;

    //[SerializeField]
    //private BoxCollider hitCollider;//当たり判定

    //[SerializeField]
    //private BoxCollider desCollider;//死亡判定

    [SerializeField]
    private float hittime = 5;//
    [SerializeField]
    private float KutibashiTime = 1;//

    private float cuntHitTime;

    private Quaternion rot;

    [SerializeField]
    private GameObject kutibasi;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        cuntHitTime = 0.0f;
        Quaternion rot = kutibasi.transform.rotation;
        floorMove = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PauseMode.start)
        {
            if (!PauseMode.start)
            {
                if (floorMove)
                {
                    //指定時間で1周する計算
                    if (time != 0)
                    {
                        theta += Mathf.PI * 2.0f * (1.0f / time) * Time.deltaTime;
                    }
                    //取得
                    float c = Mathf.Cos(theta);
                    //変換
                    c = (1.0f - c) * 0.5f;
                    //元から目的地まで往復する座標算出
                    Vector3 position = Vector3.Lerp(originalPosition, originalPosition + move, c);
                    transform.position = position;
                    //if (transform.position == originalPosition + move || transform.position == originalPosition)
                    //{
                    //    floorMove = false;
                    //    floorRotate = true;
                    //}
                }

                //if (floorRotate)
                //{
                //    //回転
                //    if (rotateTime < 0.5f)
                //    {
                //        movefloorT.Rotate(0, 360 * Time.deltaTime, 0);
                //    }
                //    rotateTime += Time.deltaTime;
                //}
                //if (rotateTime >= 0.5f)
                //{
                //    floorMove = true;
                //    floorRotate = false;
                //    rotateTime = 0;
                //}

                if (cuntHitTime >= hittime)
                {
                    cuntHitTime = 0.0f;
                    Vector3 pos = this.transform.position;
                    pos.z += -1.3f;
                    pos.y += -2.4f;
                    Instantiate(kutibasi, pos, Quaternion.Euler(0, -90, 0));
                }
                cuntHitTime += Time.deltaTime * KutibashiTime;

            }
        }
        //if (Time.frameCount%hittime==0)
        //{
        //    Vector3 pos = this.transform.position;
        //    Instantiate(kutibasi, pos,Quaternion.identity);
            
        //}
        //cuntHitTime += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        //衝突
        Player player = other.GetComponent<Player>();
        PlayerRX player2 = other.GetComponent<PlayerRX>();
        PlayerRX2 player3 = other.GetComponent<PlayerRX2>();
        PlayerRX3 player4 = other.GetComponent<PlayerRX3>();
        if (player != null || player2 != null || player3 != null || player4 != null)
        {
            //collide.enabled = false;

            Destroy(gameObject);
            floorMove = false;
            floorRotate = false;

        }
    }
}

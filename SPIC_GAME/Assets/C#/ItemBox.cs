using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]private Vector3 move = new Vector3(0, 0.1f, 0);
    [SerializeField]private float time = 1f;
    [SerializeField]private Color colors;
    [SerializeField] private Transform topBoxT;
    [SerializeField] private Transform underBoxT;
    [SerializeField] private GameObject topBox;
    [SerializeField] private GameObject underBox;
    [SerializeField] private GameObject Item;
    public static bool clearBool;
    private float clearTime;
    private bool clearRotate;
    private float clearRotateTime;
    private Vector3 originalPosition;
    private float theta;
    private Texture2D screenTexture;
    private int cunt = 0;
    Vector3 pos;
    Vector3 pos2;
    public bool isFadeOut = false;//フェードアウト処理の開始、完了を管理するフラグ

    // Start is called before the first frame update
    void Start()
    {
        clearBool = false;
        pos = topBoxT.position;
        pos2 = underBoxT.transform.position;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (clearBool)
        {
            if (clearTime < 1.0f)
            {
                //pos.y += 0.01f * clearTime;
                //pos2.y -= 0.01f * clearTime;
            }
            clearTime += Time.deltaTime;
            topBoxT.position += new Vector3(0, 0.02f * clearTime, 0);
            underBoxT.position += new Vector3(0, -0.02f * clearTime, 0);
            
            if (clearTime > 1.0f)
            {
                //originalPosition = topBoxT.position;
                //originalPosition = underBoxT.position;
                clearBool = false;
                //Instantiate(Item, transform.position, transform.rotation);
                Destroy(gameObject);
                //ここで消す
            }
        }
        if (!clearBool)
        {
            //指定時間で1周する計算
            if (time != 0)
            {
                theta += Mathf.PI * 2.0f * (1.0f / time) * Time.deltaTime;
            }
            //取得
            float c = Mathf.Sin(theta);
            //変換
            c = (1.0f - c) * 0.5f;
            //元から目的地まで往復する座標算出
            Vector3 position = Vector3.Lerp(originalPosition, originalPosition + move, c);
            transform.position = position;
        }
        if (clearRotate)
        {
            //回転
            if (clearRotateTime < 0.8f)
            {
                topBoxT.Rotate(0, 360 * Time.deltaTime, 0);
                underBoxT.Rotate(0, 360 * Time.deltaTime, 0);
            }
            clearRotateTime += Time.deltaTime;
            if (clearRotateTime >= 0.8f)
            {
                clearRotateTime = 0;
                cunt += 1;
            }
        }
    }

    //箱の下側に当たった時の処理
    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーに衝突した場合
        Player player = other.GetComponent<Player>();
        PlayerRX player2 = other.GetComponent<PlayerRX>();
        PlayerRX2 player3 = other.GetComponent<PlayerRX2>();
        if (player != null || player2 != null || player3 != null)
        {
            clearBool = true;
            clearRotate = true;
            //SceneManager.LoadScene("test");
            Instantiate(Item, transform.position, transform.rotation);
        }
    }
}


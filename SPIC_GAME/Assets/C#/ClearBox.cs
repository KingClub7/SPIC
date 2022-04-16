using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ClearBox : MonoBehaviour
{
    [SerializeField]
    private Vector3 move = new Vector3(0, 0.1f, 0);
    [SerializeField]
    private float time = 1f;

    [SerializeField]
    private Color colors;

    public static bool clearBool;

    private float clearTime;

    private bool clearRotate;

    private float clearRotateTime;

    private Vector3 originalPosition;
    private float theta;

    private Transform clearTransform;

    private Texture2D screenTexture;

    private int cunt=0;

    Vector3 pos;
    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private float fadeSpeed = 0.02f;        //透明度が変わるスピードを管理
    float red, green, blue, alfa;   //パネルの色、不透明度を管理

    public bool isFadeOut = false;//フェードアウト処理の開始、完了を管理するフラグ

    // Start is called before the first frame update
    void Start()
    {
        clearBool = false;
        pos = this.transform.position;
        clearTransform = this.transform;
        originalPosition= transform.position;

        //fadeImage = GetComponent<Image>();
        
        this.red = fadeImage.color.r;
        this.green = fadeImage.color.g;
        this.blue = fadeImage.color.b;
        alfa = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(clearBool)
        {
            if (clearTime < 1.0f)
            {
                pos.y += 0.01f * clearTime;
            }
            clearTime += Time.deltaTime;
            this.transform.position = pos;
            if(clearTime > 1.0f)
            {
                originalPosition = transform.position;
                clearBool = false;
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
            fadeImage.color = new Color(red, green, blue, alfa);//色初期化
        }

        if (clearRotate)
        {
            //回転
            if (clearRotateTime < 0.8f)
            {
                clearTransform.Rotate(0, 360 * Time.deltaTime, 0);
            }
            clearRotateTime += Time.deltaTime;
            if(clearRotateTime >= 0.8f)
            {
                clearRotateTime = 0;
                cunt+=1;
            }
        }

        if(cunt>=3)
        {
            StartFadeIn();
        }

        //Debug.Log(alfa);

    }

    //箱の下側に当たった時の処理
    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーに衝突した場合
        Player player = other.GetComponent<Player>();
        PlayerRX player2 = other.GetComponent<PlayerRX>();
        PlayerRX2 player3 = other.GetComponent<PlayerRX2>();
        PlayerRX3 player4 = other.GetComponent<PlayerRX3>();
        if (player != null || player2 != null || player3 != null || player4 != null)
        {
            clearBool = true;
            clearRotate = true;
            AudioClear.start = true;
            //SceneManager.LoadScene("test");
        }
    }

    void StartFadeIn()
    {
        alfa += fadeSpeed * Time.deltaTime;                //a)不透明度を徐々に上げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alfa >= 1)
        {                    //c)完全に透明になったら処理を抜ける
            //isFadeIn = false;
            //fadeImage.enabled = false;    //d)パネルの表示をオフにする
            SceneManager.LoadScene("Select");
        }
    }

    void SetAlpha()
    {
        fadeImage.color += new Color(red, green, blue, alfa);
    }
}

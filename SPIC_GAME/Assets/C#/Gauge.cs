using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public Image BGauge;
    private float MaxGauge = 10.0f;
    public static bool BScore;
    private float TimerC;
    private float Timer;
    private float colorChange;

    //要らないやつ
    public static bool botan;
    public static bool rockColor;
    // Start is called before the first frame update
    void Start()
    {
        BScore = false;
        BGauge.fillAmount = 0.0f;
        colorChange = 1;
        Timer = 0;
        TimerC = 0;
        botan = false;
        rockColor = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(BGauge.fillAmount);
        //ゲージ使用
        if (Input.GetKeyDown(KeyCode.P)&&BGauge.fillAmount >=1)
        {
            botan = true;
            BGauge.color += new Color(0, 0, 0, 255);
        }

        if(botan)
        {
            BGauge.fillAmount -= Time.deltaTime / Efect.EfeTimer;
            if(BGauge.fillAmount <= 0)
            {
                BGauge.fillAmount = 0;
                botan = false;
                rockColor = false;
            }
        }

        //ゲージ数え
        if (BScore)//&&BGauge.fillAmount != 1)
        {
            BGauge.fillAmount += 1/ MaxGauge;
            //Debug.Log("aaa");
            BScore = false;
        }
        //ゲージマックス字の点滅
        if(BGauge.fillAmount >= 1.0f && !botan)
        {
            rockColor = true;
            Timer += Time.deltaTime;
            //演出
            if (TimerC >= 1)
            {
                colorChange *= -1;
                TimerC = 0;
            }
            if (Timer > 0.01f)
            {
                Timer = 0;
                BGauge.color -= new Color(0, 0, 0, 0.025f * colorChange);
                //colorChange *= -1;
                TimerC += 0.025f;
                //Debug.Log(TimerC);
            }
        }
        
    }
}

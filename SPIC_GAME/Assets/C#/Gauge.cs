using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public Image BGauge;
    private float MaxGauge = 8.0f;
    public static bool BScore;
    // Start is called before the first frame update
    void Start()
    {
        BScore = false;
        BGauge.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //ゲージ数え
        if (BScore)//&&BGauge.fillAmount != 1)
        {
            BGauge.fillAmount += 1/ MaxGauge;
            //Debug.Log("aaa");
            BScore = false;
        }
        if(BGauge.fillAmount >=1.0f)
        {
            //演出
        }
        
    }
}

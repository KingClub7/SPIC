using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UISPGauge : MonoBehaviour
{
    public Image Gauge;
    private float MaxGauge = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        Gauge.fillAmount = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerRX3.jump)
        {
            if(PlayerRX3.Gmainasu>0)
            {
                if (Gauge.fillAmount <= 0)
                {
                    Gauge.fillAmount = 0;
                    PlayerRX3.jumpG = true;
                }
                else Gauge.fillAmount -= Time.deltaTime / 2;
            }
        }
        else
        {
            Gauge.fillAmount = 1.0f;
        }
        Debug.Log(Gauge.fillAmount);
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efect : MonoBehaviour
{
    public GameObject boxPrefab;
    private GameObject OBJ;
    private float EfeTime;
    private bool EfeStart;
    private float rnda;
    private float rndb;
    private int EfeTimeCount;
    private int EfeTimeMax = 10;
    public static int EfeTimer = 6;
    // Start is called before the first frame update
    void Start()
    {
        rnda = Random.Range(-0.6f, 0.6f);
        rndb = Random.Range(-0.6f, 0.6f);
        EfeTime = 0;
        EfeTimeCount = 0;
        EfeStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Gauge.botan)
        {
            EfeStart = true;
        }

        if(EfeStart)EFECT();
        if(EfeTimeCount >= EfeTimeMax*EfeTimer)
        {
            EfeStart = false;
            EfeTimeCount = 0;
        }
        //Debug.Log(EfeTimeCount);
        //Debug.Log(EfeTime);
    }
    private void EFECT()
    {
        rnda = Random.Range(-0.6f, 0.6f);
        rndb = Random.Range(-0.6f, 0.6f);
        if (EfeTime >= 0.1f)
        {
            OBJ = Instantiate(boxPrefab, transform.TransformPoint(rnda, 0, rndb), Quaternion.identity);
            OBJ.transform.parent = transform;
            EfeTime = 0;
            EfeTimeCount++;
        }
        EfeTime += Time.deltaTime;
        
    }
}

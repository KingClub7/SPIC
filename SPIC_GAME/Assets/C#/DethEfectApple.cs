using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DethEfectApple : MonoBehaviour
{
    public GameObject boxPrefab;
    public GameObject OBJ;
    private float EfeTime;
    private float rnda;
    private float rndb;
    private float rndc;
    private bool EfeStart;
    private int EfeTimeCount;
    private int EfeTimeMax = 10;
    public static int EfeTimer = 6;
    // Start is called before the first frame update
    void Start()
    {
        EfeTime = 0;
        EfeTimeCount = 0;
        EfeStart = false;
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!EfeStart)
        {
            EfeStart = true;
        }

        if (EfeStart) EFECT();
        if (EfeTimeCount >= EfeTimeMax * EfeTimer)
        {
            EfeStart = false;
            EfeTimeCount = 0;
        }
    }
    private void EFECT()
    {
        if (EfeTime >= 0.001f)
        {
            rnda = Random.Range(-0.002f, 0.002f);
            rndb = Random.Range(-0.002f, 0.002f);
            rndc = Random.Range(-0.002f, 0.002f);
            OBJ = Instantiate(boxPrefab, transform.TransformPoint(rnda, rndb, rndc), Quaternion.identity);
            //OBJ.transform.parent = transform;
            EfeTime = 0;
            EfeTimeCount++;
            Destroy(OBJ, 1.0f);
        }
        EfeTime += Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DethEfect : MonoBehaviour
{
    public GameObject[] obj = new GameObject[4];
    public int boxMax;
    private Rigidbody[] rigi = new Rigidbody[4];
    private float rnd1;
    private float rnd2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            for(int i = 0; i < boxMax; i++)
            {
                rnd1 = Random.Range(-0.2f, 0.2f);
                rnd2 = Random.Range(-0.2f, 0.2f);
                Instantiate(obj[i], transform.TransformPoint(rnd1, 0, rnd2)
                    , Quaternion.Euler(0, 0,90*i));
                //rigi[i] = obj[i].GetComponent<Rigidbody>();
                //rigi[i].AddForce(transform.forward * 0.1f, ForceMode.Force);
                Debug.Log(i);
            }
            
        }
    }
}

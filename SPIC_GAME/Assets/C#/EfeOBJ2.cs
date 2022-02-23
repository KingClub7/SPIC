using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeOBJ2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,2f*Time.deltaTime,0);
    }
}

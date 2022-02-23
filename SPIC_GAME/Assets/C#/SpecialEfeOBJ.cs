using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEfeOBJ : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-8f*Time.deltaTime,0,0);
    }
}

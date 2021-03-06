using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothness = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
        if(target!=null)
        {
            //目標地点へ補完しながら移動
            transform.position = Vector3.Lerp(transform.position, target.position, smoothness);
        }
    }
}

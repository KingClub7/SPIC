using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float smoothness = 0.1f;
    private Vector3 cameraP;
    [SerializeField] private GameObject pl;
    // Start is called before the first frame update
    void Start()
    {
        cameraP = new Vector3(0, 4, 15);
        smoothness = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pl.GetComponent<PlayerSelect>().GravityZ) smoothness = 0.002f;
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position+cameraP, smoothness);
        }
    }
}

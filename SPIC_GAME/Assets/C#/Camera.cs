using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Player pl;
    private PlayerRX pl2;
    private float smoothness = 0.1f;
    private Vector3 cameraP;
    // Start is called before the first frame update
    void Start()
    {
        cameraP = new Vector3(-1, 2, 0);
        smoothness = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (pl.plLife() >= 0 || pl2.plLife() >= 0)
        {
            if (target != null)
            {
                transform.position = Vector3.Lerp(transform.position, target.position + cameraP, smoothness);
            }
        }
    }
}
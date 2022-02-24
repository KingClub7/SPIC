using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]private Transform target;

    public static Transform Ctarget;

    [SerializeField]
    private PlayerRX pl;
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
        if (pl.plLife() >= 0)
        {
            if (Ctarget != null)transform.position = Vector3.Lerp(transform.position, Ctarget.position + cameraP, smoothness);
        }
    }
}
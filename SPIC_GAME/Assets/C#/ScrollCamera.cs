using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 move = new Vector3(0, 0.1f, 0);
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
        this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position+ move, smoothness);
    }
}

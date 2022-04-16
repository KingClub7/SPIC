using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]private Transform target;

    public static Transform Ctarget;

    [SerializeField]
    private PlayerRX pl;
    private Vector3 move = new Vector3(0, 0.2f, 0);
    private float smoothness = 0.1f;
    private Vector3 cameraP;
    private Vector3 cameraP2;
    // Start is called before the first frame update
    void Start()
    {
        cameraP = new Vector3(-1, 2, 0);
        cameraP2 = new Vector3(-1, 0, -8);
        smoothness = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(StageSelect.SelectStage >= 4 && !ClearBox.clearBool)
        {
            if (pl.plLife() >= 0)
            {
                Vector3 T = new Vector3(Ctarget.position.x, this.transform.position.y, cameraP2.z);
                this.transform.position = Vector3.Lerp(this.transform.position, T + move, smoothness);
            }
            
        }
        else
        {
            if (pl.plLife() >= 0)
            {
                if (Ctarget != null) transform.position = Vector3.Lerp(transform.position, Ctarget.position + cameraP, smoothness);
            }
        }
        
    }
}
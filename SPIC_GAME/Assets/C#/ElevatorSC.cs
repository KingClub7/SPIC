using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSC : MonoBehaviour
{
    public Transform PushTR;
    public Transform PushTL;
    private Vector3 originalT;
    [SerializeField]private float move = 1;
    private int moveCount;
    private float moveMax = 5;
    private float Timer;
    private float fallTimer;
    private int fallTimerCount;
    private bool moveOver;
    private bool fall;

    public bool elevatorSL;
    public bool elevatorSR;
    public static bool PlayerJump;

    public Collider PushCo;
    // Start is called before the first frame update
    void Start()
    {
        fallTimer = 0;
        Timer = 0;
        PlayerJump = false;
        moveCount = 0;
        moveOver = false;
        fall = false;
        //PushCo.enabled = false;
        PushCo = GetComponent<Collider>();
        originalT = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(elevatorSL && !moveOver && moveCount > 0)
         {
            moveCount--;
            transform.Translate(0, -move, 0);
            ///PushTL.transform.localPosition += new Vector3(0, 0.003f, 0);
            //ElevatorSL.elevatorSL = false;
            //PlayerJump = false;
        }
        if(elevatorSR && !moveOver && moveCount < moveMax)
         {
            moveCount++;
            transform.Translate(0, move, 0);
            //PushTR.transform.localPosition += new Vector3(0, 0.003f, 0);
            //ElevatorSR.elevatorSR = false;
            //PlayerJump = false;
         }
        Debug.Log(moveCount);
        //スイッチ移動
        //if (PushTL.transform.localPosition.y > 0.013f)
        //{
        //    PushTL.transform.localPosition += new Vector3(0, -0.01f*Time.deltaTime, 0);
        //}
        //if (PushTR.transform.localPosition.y > 0.013f)
        //{
        //    PushTR.transform.localPosition += new Vector3(0, -0.01f*Time.deltaTime, 0);
        //}
        //Debug.Log(PushT.transform.localPosition.y);
        elevatorSR = false;
        elevatorSL = false;
    }
    //private void OnTriggerEnter(Collider other)
    private void OnTriggerStay(Collider other)
    {
        //衝突
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            PushCo.enabled = true;
            
            //if(PlayerJump && moveCount < moveMax)
            //{
            //    moveCount++;
            //    transform.Translate(0, move, 0);
            //    PlayerJump = false;
            //}
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (moveCount >= moveMax)
    //    {
    //        moveOver = true;
    //        ElevatorS.elevatorS = false;
    //        moveCount = 0;
    //    }
    //}
}

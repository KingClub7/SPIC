using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kutibasi : MonoBehaviour
{
    //private BoxCollider controller;
    [SerializeField]
    private float maxFallSpeed = 10.0f;//最大落下速度

    [SerializeField]
    private float gravity = 60.0f;//重力

    private float verticalSpeed; //垂直移動度

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGravity();
        UpdateMovement();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Enemy2 enemy2 = other.GetComponent<Enemy2>();
        //if(enemy2==null)
        Destroy(gameObject);
    }



    private void UpdateGravity()
    {

        
        verticalSpeed -= gravity * Time.deltaTime;
        
        //垂直移動速度が最大落下速度を超えないように制限
        verticalSpeed = Mathf.Max(verticalSpeed, -maxFallSpeed);
    }

    private void UpdateMovement()
    {


        //移動量を計算
        Vector3 move = new Vector3(0, verticalSpeed, 0);



        //移動処理(この関数内でOnControllerColliderHit関数が呼ばれる)
        this.transform.position+=move * Time.deltaTime;


    }
}

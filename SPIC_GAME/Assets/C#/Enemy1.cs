using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class Enemy1 : MonoBehaviour
{

    [SerializeField]
    private Vector3 move = new Vector3(-2f, 0, 0);
    [SerializeField]
    private float time = 1f;

    private Vector3 originalPosition;
    private float theta;
    [SerializeField]
    private bool floorMove;
    private bool floorRotate;

    [SerializeField]
    private Transform movefloorT;

    //[SerializeField]
    //private GameObject buttonRotate;

    private float rotateTime;

    private Player pl;

    [SerializeField]
    private Collider collide;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        GameObject obj = GameObject.Find("Player");
        pl = obj.GetComponent<Player>();
        floorMove = true;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (pl.PlayerUpdateBool())
        //{
            if (floorMove)
            {
                Debug.Log("aaa");
                //指定時間で1周する計算
                if (time != 0)
                {
                    theta += Mathf.PI * 2.0f * (1.0f / time) * Time.deltaTime;
                }
                //取得
                float c = Mathf.Cos(theta);
                //変換
                c = (1.0f - c) * 0.5f;
                //元から目的地まで往復する座標算出
                Vector3 position = Vector3.Lerp(originalPosition, originalPosition + move, c);
                transform.position = position;
                if(transform.position== originalPosition+move|| transform.position == originalPosition)
                {
                    floorMove = false;
                    floorRotate=true;
                }
            }

            if (floorRotate)
            {
                //回転
                if (rotateTime < 0.5f)
                {
                    movefloorT.Rotate(0, 180 * 2 * Time.deltaTime, 0);
                }
                rotateTime += Time.deltaTime;
            }
            if (rotateTime >= 0.5f)
            {
                floorMove = true;
                floorRotate = false;
                rotateTime = 0;
            }
        
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        //衝突
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            //collide.enabled = false;

            Destroy(gameObject);
            floorMove = false;
            floorRotate = false;
          
        }
    }

    


}

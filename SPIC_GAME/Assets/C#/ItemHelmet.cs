using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHelmet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1 * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーに衝突した場合
        Player player = other.GetComponent<Player>();
        PlayerRX player2 = other.GetComponent<PlayerRX>();
        PlayerRX2 player3 = other.GetComponent<PlayerRX2>();
        //PlayerRX3 player4 = other.GetComponent<PlayerRX3>();
        if (player != null)
        {
            Player.levelupbool = true;
            Destroy(gameObject);
        }
        if (player2 != null)
        {
            PlayerRX.levelupbool = true;
            Destroy(gameObject);
        }
        if (player3 != null)
        {
            PlayerRX2.levelupbool = true;
            Destroy(gameObject);
        }
        //if (player4 != null)
        //{
        //    PlayerRX3.levelupbool = true;
        //    Destroy(gameObject);
        //}
    }
}

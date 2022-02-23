using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    private void OnTriggerEnter(Collider other)
    {
        //ƒvƒŒƒCƒ„[€–Sˆ—
        Player player = other.GetComponent<Player>();
        PlayerRX player2 = other.GetComponent<PlayerRX>();
        PlayerRX2 player3 = other.GetComponent<PlayerRX2>();
        PlayerRX3 player4 = other.GetComponent<PlayerRX3>();
        if (player != null)player.Death();
        if (player2 != null)player2.Death();
        if (player3 != null)player2.Death();
        if (player4 != null)player2.Death();
    }
}

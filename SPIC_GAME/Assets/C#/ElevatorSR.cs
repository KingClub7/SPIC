using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSR : MonoBehaviour
{
    //public static bool elevatorSR;
    [SerializeField] private GameObject ev;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //衝突
        Player player = other.GetComponent<Player>();
        PlayerRX player2 = other.GetComponent<PlayerRX>();
        PlayerRX2 player3 = other.GetComponent<PlayerRX2>();
        PlayerRX3 player4 = other.GetComponent<PlayerRX3>();
        if (player != null || player2 != null || player3 != null || player4 != null)
        {
            ev.GetComponent<ElevatorSC>().elevatorSR = true;
            //elevatorSR = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSL : MonoBehaviour
{
    //public static bool elevatorSL;
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
        if (player != null)
        {
            ev.GetComponent<ElevatorSC>().elevatorSL = true;
            //elevatorSL = true;
        }
    }
}

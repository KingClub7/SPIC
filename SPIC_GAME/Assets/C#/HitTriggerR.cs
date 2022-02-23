using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTriggerR : MonoBehaviour
{
    [SerializeField]
    private Object obj;
    public GameObject desPrefab;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
    private void OnTriggerEnter(Collider other)
    {
        //衝突
        Player player = other.GetComponent<Player>();
        PlayerRX player2 = other.GetComponent<PlayerRX>();
        PlayerRX2 player3 = other.GetComponent<PlayerRX2>();
        PlayerRX3 player4 = other.GetComponent<PlayerRX3>();
        if (player != null)
        {
            //collide.enabled = false;
            if(player.plLife()>=0)Destroy(obj);
            Instantiate(desPrefab, transform.position, transform.rotation);
            //player.Death();
        }
        if (player2 != null)if(player2.plLife()>=0)
            {
                if (player2.plLife() >= 0) Destroy(obj);
                Instantiate(desPrefab, transform.position, transform.rotation);
            }
        if (player3 != null)if(player3.plLife()>=0)
            {
                if (player3.plLife() >= 0) Destroy(obj);
                Instantiate(desPrefab, transform.position, transform.rotation);
            }
        if (player4 != null)if(player4.plLife()>=0)
            {
                if (player4.plLife() >= 0) Destroy(obj);
                Instantiate(desPrefab, transform.position, transform.rotation);
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitTrigger : MonoBehaviour
{
    [SerializeField]
    private Object obj;
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
        if (player != null)
        {
            //collide.enabled = false;

            
            Destroy(obj);

            //player.Death();
        }
    }
}

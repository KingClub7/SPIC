using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakB_R : MonoBehaviour
{
    [SerializeField]
    private float impulse = 4.0f;
    [SerializeField]
    private GameObject destroyBoxPrefab;
    [SerializeField] private float Destime = 0.7f;
    [SerializeField] private Material rockMate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Gauge.rockColor)
        {
            rockMate.color = new Color(1f, 1f, 1f ,0);
        }
        else
        {
            rockMate.color = new Color(0.3f, 0.35f, 0.35f ,0);
        }
        
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
            //Debug.Log("bbb");
            GameObject destroyBox = Instantiate(destroyBoxPrefab,
                                    transform.position, transform.rotation);
            //破片列挙
            foreach (Rigidbody body in destroyBox.GetComponentsInChildren<Rigidbody>())
            {
                body.AddForce(
                    Random.Range(-impulse, impulse),
                    Random.Range(0.0f, impulse),
                    Random.Range(-impulse, impulse),
                    ForceMode.Impulse
                    );
            };
            Destroy(destroyBox, Destime);
            Destroy(gameObject);
        }
    }
}

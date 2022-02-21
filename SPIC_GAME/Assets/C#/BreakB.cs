using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakB : MonoBehaviour
{
    [SerializeField]
    private float impulse = 4.0f;
    [SerializeField]
    private GameObject destroyBoxPrefab;
    [SerializeField] private float Destime = 0.7f;

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

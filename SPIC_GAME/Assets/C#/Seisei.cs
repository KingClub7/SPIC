using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seisei : MonoBehaviour
{
    [SerializeField]
    private GameObject SeiseiPrefab;
    private GameObject OBJ;

    bool hakai;

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
            if(hakai)
            {
                //foreach(Transform child in gameObject.transform)
                //{
                //    Destroy(child.gameObject);
                //}
                DestroyOBJ();
                hakai = false;
            }
            else if(!hakai)
            {
                //Instantiate(SeiseiPrefab, transform.TransformPoint(0,2.5f,0), Quaternion.identity);
                //SeiseiPrefab.transform.SetParent(transform);
                OBJInstant();
                hakai = true;
            }
        }
    }
    private void DestroyOBJ()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
    private void OBJInstant()
    {
        //Instantiate(SeiseiPrefab, transform.TransformPoint(0, 2.5f, 0), Quaternion.identity,transform);
        OBJ = Instantiate(SeiseiPrefab, transform.TransformPoint(0, 2.5f, 0), Quaternion.identity);
        OBJ.transform.parent = transform;
    }
}

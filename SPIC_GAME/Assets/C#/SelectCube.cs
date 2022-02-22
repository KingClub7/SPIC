using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectCube : MonoBehaviour
{
    [SerializeField] private GameObject playerG;
    private float turn;
    private float timer;
    private bool select;
    private int stage;
    [SerializeField] private GameObject pl;
    // Start is called before the first frame update
    void Start()
    {
        turn = 20;
        select = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * turn, 0);
        if(select)
        {
            timer += Time.deltaTime;
            if(timer>1f)
            {
                switch(stage)
                {
                    case 1: 
                        SceneManager.LoadScene("Stage1");
                        break;
                    case 2: 
                        SceneManager.LoadScene("SampleScene");
                        break;
                    case 3: 
                        SceneManager.LoadScene("SampleScene");
                        break;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //衝突
        PlayerSelect player = other.GetComponent<PlayerSelect>();
        if (player != null)
        {
            turn = 300;
            Debug.Log("get");
            select = true;
            stage = pl.GetComponent<PlayerSelect>().moveCount;
            Destroy(playerG);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectCube : MonoBehaviour
{
    //[SerializeField] private GameObject playerG;
    private float turn;
    private float timer;
    private bool select;
    private int stage;
    private bool space;
    [SerializeField] private GameObject pl;
    // Start is called before the first frame update
    void Start()
    {
        turn = 20;
        select = false;
        timer = 0;
        space = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!space) stage = PlayerSelect.moveCount;
        transform.Rotate(0, Time.deltaTime * turn, 0);
        if(select)
        {
            timer += Time.deltaTime;
            if(timer>1f)
            {
                if(stage<=0) SceneManager.LoadScene("Setsumei");
                else if (stage > 0)
                {
                    StageSelect.SelectStage = stage;
                    SceneManager.LoadScene("StageTitle");
                }
                //switch(stage)
                //{
                //    case 1: 
                //        SceneManager.LoadScene("Stage1");
                //        break;
                //    case 2: 
                //        SceneManager.LoadScene("Stage2");
                //        break;
                //    case 3: 
                //        SceneManager.LoadScene("SampleScene");
                //        break;
                //}
            }
        }
        Debug.Log(stage);
    }
    private void OnTriggerEnter(Collider other)
    {
        //衝突
        PlayerSelect player = other.GetComponent<PlayerSelect>();
        if (player != null)
        {
            space = true;
            turn = 300;
            Debug.Log("get");
            select = true;
            //stage = pl.GetComponent<PlayerSelect>().moveCount;
            //stage = PlayerSelect.moveCount;
            Destroy(pl);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DethScene : MonoBehaviour
{
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerRX.DesBool) timer += Time.deltaTime;
        if(timer>1.5f)
        {
            timer = 0;
            //PlayerRX.DesBool = false;
            SceneManager.LoadScene("StageTitle");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMode : MonoBehaviour
{
    private float sentaku;
    private bool start;
    private bool returnSelect;
    // Start is called before the first frame update
    void Start()
    {
        returnSelect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter)&&!start)
        {
            //Time.timeScale = 0;
            start = true;
        }
        if(start)
        {
            sentaku = Input.GetAxisRaw("Vertical");
            if(sentaku>0)
            {
                returnSelect = false;
            }
            if(sentaku<0)
            {
                returnSelect = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && start)
        {
            if(returnSelect)
            {
                start = false;
                //Time.timeScale = 1f;
                SceneManager.LoadScene("Select");
            }
            else
            {
                Time.timeScale = 1f;
                start = false;
                returnSelect = false;
            }
        }
    }
}

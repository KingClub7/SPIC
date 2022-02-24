using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PauseMode : MonoBehaviour
{
    private float sentaku;
    public static bool start;
    private bool start2;
    private bool returnSelect;
    public RectTransform IconT;
    public Image IBack;
    public Image IFront;
    public Image IIcon;
    // Start is called before the first frame update
    void Start()
    {
        returnSelect = false;
        ImageColor(false);
        start = false;
    }

    // Update is called once per frame
    void Update()
    { 
        
        if (start)
        {
            sentaku = Input.GetAxisRaw("Vertical");
            if (sentaku > 0)
            {
                if(returnSelect)
                {
                    IconT.position += new Vector3(0, 200, 0); 
                    returnSelect = false;
                }
                
            }
            if (sentaku < 0)
            {
                if (!returnSelect)
                {
                    IconT.position += new Vector3(0, -200, 0);
                    returnSelect = true;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.Return) && start)
        {
            if (returnSelect)
            {
                start = false;
                start2 = true;
                //Time.timeScale = 1f;
                ImageColor(false);
                SceneManager.LoadScene("Select");
            }
            else
            {
                //Time.timeScale = 1f;
                start = false;
                start2 = true;
                ImageColor(false);
                returnSelect = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && !start&& !start2)
        {
            //Time.timeScale = 0;
            start = true;
            ImageColor(true);
        }
        start2 = false;
    }
    private void ImageColor(bool alpha)
    {
        if(alpha)
        {
            IBack.color += new Color(0, 0, 0, 1);
            IFront.color += new Color(0, 0, 0, 1);
            IIcon.color += new Color(0, 0, 0, 1);
        }
        else if(!alpha)
        {
            IBack.color += new Color(0, 0, 0, -1);
            IFront.color += new Color(0, 0, 0, -1);
            IIcon.color += new Color(0, 0, 0, -1);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    public static int SelectStage;
    private float timer;
    public Image[] images = new Image[3];
    public int imageMax = 3;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i<imageMax;i++)
        {
            var c = images[i].color;
            images[i].color = new Color(c.r, c.g, c.b, 0); 
        }
        var c2 = images[SelectStage-1].color;
        images[SelectStage-1].color = new Color(c2.r, c2.g, c2.b, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>1)
        {
            switch (SelectStage)
            {
                case 1:
                    SceneManager.LoadScene("Stage1");
                    break;
                case 2:
                    SceneManager.LoadScene("Stage2");
                    break;
                case 3:
                    SceneManager.LoadScene("Stage3");
                    break;
                case 4:
                    SceneManager.LoadScene("Stage4");
                    break;
            }
        }
        timer += Time.deltaTime;
    }
}

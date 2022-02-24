using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Setsumei : MonoBehaviour
{
    public Image[] images = new Image[5];
    private int cunt;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            var c = images[i].color;
            images[i].color = new Color(c.r, c.g, c.b, 0);
            cunt = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Hyouji();
        if(Input.GetKeyDown(KeyCode.Return))
        {
            cunt++;
        }
        if (cunt > 4) SceneManager.LoadScene("Select");
    }
    private void Hyouji()
    {
        for (int i = 0; i < 5; i++)
        {
            if(i == cunt)
            {
                var c = images[cunt].color;
                images[cunt].color = new Color(c.r, c.g, c.b, 1);
                break;
            }
            else
            {
                var c = images[i].color;
                images[i].color = new Color(c.r, c.g, c.b, 0);
            }
        }
        
    }
}

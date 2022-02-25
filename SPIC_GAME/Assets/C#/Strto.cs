using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Strto : MonoBehaviour
{
    private float timres;

    private bool srt;

    [SerializeField]
    private Color co;

    private float alpha; 
    // Start is called before the first frame update
    void Start()
    {
        srt = false;
        timres = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (srt == false)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                srt = true;
                //EditorSceneManager.LoadScene(0);
            }

            if (Input.GetKey(KeyCode.Backspace))
            {
                UnityEditor.EditorApplication.isPlaying = false;

                Application.Quit();
            }
        }
        if(srt==true)
        {
            timres += Time.deltaTime;
            
        }
        if (co != null)
        {
            co = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }

        if(timres>1.0f)
        {
            EditorSceneManager.LoadScene(0);
        }

    }
}

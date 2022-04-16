using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Title : MonoBehaviour
{
    private float timer;
    private bool start;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            start = true;
        }
        if (start) timer += Time.deltaTime;
        if (timer >= 1f) SceneManager.LoadScene("Select");
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}

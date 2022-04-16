using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClear : MonoBehaviour
{
    private AudioSource audio_Clear;
    public static bool start;
    private bool stop;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        stop = false;
        audio_Clear = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stop) start = false;
        if(start)
        {
            audio_Clear.Play();
            //start = false;
            stop = true;
        }
    }
}

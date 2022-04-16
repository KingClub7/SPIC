using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource audio_BGM;
    // Start is called before the first frame update
    void Start()
    {
        audio_BGM = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioClear.start)
        {
            audio_BGM.Stop();
        }
    }
}

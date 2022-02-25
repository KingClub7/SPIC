using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Leveldown : MonoBehaviour
{
    private AudioSource audio_leveldown;
    bool LeveldownAudioStart = false;

    // Start is called before the first frame update
    void Start()
    {
        audio_leveldown = GetComponent<AudioSource>();
        audio_leveldown.Play();
        LeveldownAudioStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio_leveldown.isPlaying && LeveldownAudioStart)
        {
            Destroy(gameObject);
        }
    }
}

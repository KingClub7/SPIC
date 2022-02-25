using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_levelup : MonoBehaviour
{
    private AudioSource audio_levelup;
    bool LevelupAudioStart = false;

    // Start is called before the first frame update
    void Start()
    {
        audio_levelup = GetComponent<AudioSource>();
        audio_levelup.Play();
        LevelupAudioStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!audio_levelup.isPlaying&& LevelupAudioStart)
        {
            Destroy(gameObject);
        }
    }
}

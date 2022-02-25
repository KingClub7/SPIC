using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_enter : MonoBehaviour
{
    private AudioSource audio_enter;
    public AudioClip enter;//決定のサウンド
    

    // Start is called before the first frame update
    void Start()
    {
        audio_enter = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            audio_enter.PlayOneShot(enter);

        }
    }
}

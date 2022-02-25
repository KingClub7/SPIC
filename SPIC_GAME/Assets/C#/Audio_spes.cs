using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_spes : MonoBehaviour
{
    private AudioSource audio_spes;
    public AudioClip enter;//決定のサウンド


    // Start is called before the first frame update
    void Start()
    {
        audio_spes = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audio_spes.PlayOneShot(enter);

        }
    }
}

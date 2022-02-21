using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClearUI : MonoBehaviour
{
    private Animator clearMoji;
    private float move;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        this.clearMoji = GetComponent<Animator>();
        clearMoji.SetBool("Clear", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ClearBox.clearBool) clearMoji.SetBool("Clear", true);
    }
}

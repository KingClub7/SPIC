using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UISPGaugePos : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]
    //private Transform targetTfm;
    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 0.2f, 0);
    private Image image;

    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (PlayerRX3.islive)
        {
            myRectTfm.position = RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main, Camera.Ctarget.position + offset);
            if (PlayerRX3.jump)
            {
                var c = image.color;
                image.color = new Color(c.r, c.g, c.b, 1);
            }
            else
            {
                var c = image.color;
                image.color = new Color(c.r, c.g, c.b, 0);
            }
        }
        else
        {
            var c = image.color;
            image.color = new Color(c.r, c.g, c.b, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlinkEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Color startColor = Color.red;
    public Color endColor = Color.white;
    [Range(0, 10)]
    public float speed = 1;
    public static bool onRecord = false;

    Image imgComp;

    void Awake()
    {
        imgComp = GetComponent<Image>();
    }

    void Update()
    {
        if (onRecord)
        {
            imgComp.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
        }
        else
        {
            imgComp.color = Color.red;
        }
    }



}

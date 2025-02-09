using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class RecordingPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Color startColor = Color.red;
    public Color endColor = Color.white;
    [Range(0, 10)]
    public float speed = 1;
    public static bool onRecord = false;
    public TMP_Text timerTxt;
    [SerializeField] TMP_Text recTxt;
    Coroutine coroutine;

    [SerializeField] Image blinkingButton;

    void Awake()
    {
        recTxt.enabled = false;
        stopRecord(); // reset record

    }

    void Update()
    {
        if (onRecord)
        {
            // blinking red button 
            blinkingButton.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
        }
 
    }

    IEnumerator DoTimer(float counttime = 1f)
    {
        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(counttime);
            count++;
            TimeSpan time = TimeSpan.FromSeconds(count);
            string formattedTime = time.ToString(@"mm\:ss");
            timerTxt.text = formattedTime;
        }
    }

    public void startRecord()
    {
        // time for recording
        onRecord = true;
        recTxt.enabled = true;
        coroutine = StartCoroutine(DoTimer());
    }


    public void stopRecord()
    {
        onRecord = false;
        recTxt.enabled = false;
        blinkingButton.color = Color.red;
        timerTxt.text = string.Empty;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }


}

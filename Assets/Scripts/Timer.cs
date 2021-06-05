using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text timerText;
    private float timer;    
    public string TimerString => ConvertSecondsToSring(timer);

    void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;    
        //вывод отформатированного времени в текст
        timerText.text = ConvertSecondsToSring(timer);
    }

    private string ConvertSecondsToSring(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }    
}

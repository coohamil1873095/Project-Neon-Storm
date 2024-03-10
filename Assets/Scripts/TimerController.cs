using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private TimeSpan timePlaying;
    private bool timerActive = false;
    private float elapsedTime;

    public void StartTimer()
    {
        timerActive = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (timerActive)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timerStr = timePlaying.ToString("mm':'ss");
            timerText.SetText(timerStr);

            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int finishTimeInSecs;
    private TimeSpan timePlaying;
    private bool timerActive = false;
    private float elapsedTime;

    void Update()
    {
        if (elapsedTime >= finishTimeInSecs) 
        {
            timerActive = false;
            elapsedTime = 0f;
            GameManager.Instance.EndCurrentGame(true);
        }
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    float timer;
    public TMP_Text gameTimeText;
    public bool updateTimer = true;

    void Update()
    {
        if (updateTimer) {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
    }

    private void UpdateTimerDisplay(float time) {
        float seconds = (int)(time % 60);
        int minutes = (int)(time / 60) % 60;
        int hours = (int)(time / 3600) % 24;

        string currentTime = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);
        gameTimeText.text = "Время прохождения: " + currentTime;
    }
}

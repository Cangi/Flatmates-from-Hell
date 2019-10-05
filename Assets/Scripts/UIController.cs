using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public TextMeshProUGUI cleanLevelText;

    public float time = 300f;

    public int minutes;

    public int seconds;

    public int cleanLevel;

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        minutes = Mathf.RoundToInt(time / 60) - 1;
        seconds = Mathf.RoundToInt(time % 60);
        if (seconds < 10)
        {
            timerText.text = "Time: " + minutes + ":0" + seconds;
        }
        else
        {
            timerText.text = "Time: " + minutes + ":" + seconds;
        }

        cleanLevelText.text = "Clean Level: " + cleanLevel;

    }
}

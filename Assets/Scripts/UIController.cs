using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public TextMeshProUGUI cleanLevelText;

    public float time = 300f;

    public int minutes;

    public int seconds;

    public int cleanLevel;

    public GameObject cleaningObjects;

    private List<CleaningScript> cleaningScripts;

    
    private void Start()
    {
        
        cleaningScripts = new List<CleaningScript>();
        foreach (Transform child in cleaningObjects.transform)
        {
            cleaningScripts.Add(child.gameObject.GetComponent<CleaningScript>());
        }
    }

    public void changeCleanliness()
    {
        float count = 0;
        float total = 0;
        foreach (CleaningScript script in cleaningScripts)
        {
            total++;
            if (script.isDirty())
            {
                count++;
            }
        }

        cleanLevel = 100 - (int)((float)(count / total * 100));
    }
    
    // Update is called once per frame
    void Update()
    {

        time -= Time.deltaTime;
        minutes = (int)time / 60;
        seconds = (int)time;
        if (seconds < 10)
        {
            timerText.text = "Time: " + minutes + ":0" + seconds;
        }
        else
        {
            timerText.text = "Time: " + minutes + ":" + seconds;
        }

        cleanLevelText.text = "Clean Level: " + cleanLevel + '%';

        if (time<=0)
        {
            if (cleanLevel != 100)
            {
                SceneManager.LoadScene("Lose");
            }
            else
            {
                SceneManager.LoadScene("Win");
            }
        }
        
        
    }
}

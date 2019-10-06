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

    private List<Transform> dirtyObjects;

    private Transform character;
    private Transform closest;
    public Transform arrow;


    
    private void Start()
    {
        character = FindObjectOfType<CameraScript>().gameObject.transform;
        cleaningScripts = new List<CleaningScript>();
        dirtyObjects = new List<Transform>();
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
                if (!dirtyObjects.Contains((script.transform)))
                {
                    dirtyObjects.Add(script.transform);
                }
                else
                {
                    dirtyObjects.Remove(script.transform);
                }
            }
        }

        cleanLevel = 100 - (int)((float)(count / total * 100));
    }
    
    // Update is called once per frame
    void Update()
    {
        float closestDistance = 999f;
        time -= Time.deltaTime;
        minutes = (int)time / 60;
        seconds = (int)time - minutes * 60;
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

        if (closest == null)
        {
            foreach (Transform dirtyObject in dirtyObjects)
            {
                if (Vector3.Distance(character.position, dirtyObject.position) < closestDistance)
                {
                    closestDistance = Vector3.Distance(character.position, dirtyObject.position);
                    closest = dirtyObject;
                }
            }
        }
        else
        {
            Vector3 t = character.position - closest.position;
            arrow.transform.right = closest.position - character.position;
            
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    public Sprite secondImage;
    private bool secondStage;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!secondStage)
            {
                secondStage = true;
                GetComponent<Image>().sprite = secondImage;
            }
            else
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}

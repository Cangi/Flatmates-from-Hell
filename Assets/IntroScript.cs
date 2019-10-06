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
            var sceneName = SceneManager.GetActiveScene().name;
            if (!secondStage && sceneName !="Lose" && sceneName != "Win") 
            {
                secondStage = true;
                GetComponent<Image>().sprite = secondImage;
                var loseText = GameObject.Find("LoseText");
                if(loseText) {
                    Text tRef = loseText.GetComponent<Text>(); 
                    tRef.text = "";
                }
                
            }
            else
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}

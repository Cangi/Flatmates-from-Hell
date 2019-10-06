using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLoseText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int loseClean = StaticClass.crossSceneCleanliness;

        string loseText = "Your flat was only " + loseClean.ToString() + "% clean :(";
        
        Text textRef = gameObject.GetComponent(typeof(Text)) as Text;

        textRef.text = loseText;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameSpawner : MonoBehaviour
{

    private TextMeshProUGUI nameObject;

    public string charName;
    
    // Start is called before the first frame update
    void Start()
    {
        NameManager nameMan = GameObject.Find("NameManager").GetComponent(typeof(NameManager)) as NameManager;
        charName = nameMan.retrieveName();
        Debug.Log(charName);
        nameObject = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        nameObject.text = charName;
    }
}

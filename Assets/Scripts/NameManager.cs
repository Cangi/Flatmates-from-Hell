using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NameManager : MonoBehaviour
{
    public List<string> namePool;

    // Start is called before the first frame update
    void Awake()
    {
        namePool = new List<string>();
        
        namePool.Add("Rony");
        namePool.Add("Paulius");
        namePool.Add("Radu");
        namePool.Add("Dwayne");
        namePool.Add("Andrei");
        namePool.Add("Loginovsz");
        namePool.Add("Robertas");
        namePool.Add("Ignas");
    }

    public string retrieveName()
    {
        string name = "";
        Debug.Log(namePool.Count);
        if (namePool.Count > 0)
        {
            int indicator = Random.Range(0, namePool.Count - 1);
            name = namePool[indicator];
            namePool.Remove(name);
        }

        return name;
    }
}

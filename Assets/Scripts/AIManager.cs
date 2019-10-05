using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public List<GameObject> availableTasks;
    public List<GameObject> reservedTasks;
    public List<GameObject> completedTasks;
    
    // Start is called before the first frame update
    void Start()
    {
        availableTasks = new List<GameObject>();
        reservedTasks = new List<GameObject>();
        completedTasks = new List<GameObject>();
    }

    public void addAvailableTask(GameObject _obj)
    {
        availableTasks.Add(_obj);
    }

    public GameObject reserveATask()
    {
        if (availableTasks.Count > 0)
        {
            GameObject reservedObject = availableTasks[0];
            availableTasks.Remove(reservedObject);
            reservedTasks.Add(reservedObject);
            return reservedObject;
        }
        else
        {
            return null;
        }
    }

    public void notifyCompletion(GameObject _obj)
    {
        reservedTasks.Remove(_obj);
        completedTasks.Add(_obj);
    }
    
    public void notifyCleanedUp(GameObject _obj)
    {
        completedTasks.Remove(_obj);
        availableTasks.Add(_obj);
    }
}

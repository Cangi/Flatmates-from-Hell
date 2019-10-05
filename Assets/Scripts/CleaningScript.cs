using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningScript : MonoBehaviour
{
    public GameObject spacebar;
    private GameObject buttonInstance;
    private Vector2 startScale;
    private bool pulsating, inTrigger;
    private float pulseSpeed = 10;
    private float pulsationThreshold = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        buttonInstance = Instantiate(spacebar, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
        startScale = buttonInstance.transform.localScale;
        pulsating = true;
        inTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        pulsating = false;
        inTrigger = false;
        Destroy(buttonInstance);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger)
        {
            if (pulsating)
            {
                buttonInstance.transform.localScale = new Vector2(
                    buttonInstance.transform.localScale.x + pulseSpeed * Time.deltaTime,
                    buttonInstance.transform.localScale.y + pulseSpeed * Time.deltaTime);
                if (buttonInstance.transform.localScale.x > startScale.x + pulsationThreshold ||
                    buttonInstance.transform.localScale.x < startScale.x - pulsationThreshold)
                {
                    pulseSpeed *= -1;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Sprite doorOpened;

    public Sprite doorClosed;
    private SpriteRenderer spriteR;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        isOpen = false;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("entered trigger!");
        if (!isOpen)
        {
            spriteR.sprite = doorOpened;
            isOpen = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (isOpen)
        {
            spriteR.sprite = doorClosed;
            isOpen = false;
        }
    }
}

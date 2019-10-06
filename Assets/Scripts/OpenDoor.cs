using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Sprite doorOpened;

    public Sprite doorClosed;
    private SpriteRenderer spriteR;
    private bool isOpen;
    private AudioSource audio;
    public List<AudioClip> sounds;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        isOpen = false;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (!isOpen)
        {
            spriteR.sprite = doorOpened;
            isOpen = true;
            audio.clip = sounds[Random.Range(0, sounds.Count)];
            audio.Play();
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

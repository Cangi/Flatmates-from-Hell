using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CleaningScript : MonoBehaviour
{
    private GameObject spacebar;
    public int cleaningTime = 5;
    private GameObject cleaned;
    private Transform character;
    public Sprite cleanSprite;
    public Sprite dirtySprite;
    private int timeLeft;
    private GameObject buttonInstance;
    private GameObject cleanedInstance;
    private SpriteRenderer buttonInstanceSprite;
    private Vector2 startScale;
    private bool pulsating, inTrigger, cleaning;
    private float pulseSpeed = 10;
    private float pulsationThreshold = 2;
    private Dictionary<char, Sprite> keyboard;
    private char randomChar;
    private bool dirty;
    private UIController uic;
    private AIManager aim;
    public void dirtyUp()
    {
        dirty = true;
        GetComponent<SpriteRenderer>().sprite = dirtySprite;
        aim.notifyCompletion(gameObject);
        uic.changeCleanliness();
    }

    public bool isDirty()
    {
        return dirty;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dirty)
        {
            buttonInstance = Instantiate(spacebar, new Vector3(transform.position.x, transform.position.y, -1),
                Quaternion.identity);
            startScale = buttonInstance.transform.localScale;
            buttonInstanceSprite = buttonInstance.GetComponent<SpriteRenderer>();
            pulsating = true;
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        removeButton();
    }

    
    // Start is called before the first frame update
    void Start()
    {
        keyboard = new Dictionary<char, Sprite>();
        spacebar = Resources.Load<GameObject>("Buttons/spacebar");
        cleaned = Resources.Load<GameObject>("cleaned");
        var keyboardArray = Resources.LoadAll("Buttons/Keyboard", typeof(Sprite)).Cast<Sprite>().ToArray();
        foreach (var key in keyboardArray)
        {
            keyboard.Add(key.name.ToCharArray()[0], key);
        }

        timeLeft = cleaningTime;
        uic = GameObject.Find("Player").GetComponent<UIController>();
        aim = GameObject.Find("Player").GetComponent<AIManager>();
        aim.addAvailableTask(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // TEST
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameObject.Find("stove").GetComponent<CleaningScript>().dirtyUp();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            GameObject.Find("bin").GetComponent<CleaningScript>().dirtyUp();
        }
        // END TEST
        
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

            if (Input.GetKeyDown(KeyCode.Space) && !cleaning)
            {
                cleaning = true;

                generateKey();

            }
            if (cleaning && Input.GetKeyDown(randomChar.ToString()))
            {
                generateKey();
            }
        }
    }

    void generateKey()
    {
        
        char[] forbiddenKeys = new char[] {'a', 's', 'w', 'd'};
        randomChar = 'a';
        char lastKey = randomChar;
        while (forbiddenKeys.Contains(randomChar) || lastKey == randomChar)
        {
            lastKey = randomChar;
            randomChar = (char) Random.Range('a', 'z');
            
        }


        buttonInstanceSprite.sprite = keyboard[randomChar];

        timeLeft--;
        if (timeLeft == 0) // CLEANED! 
        {
            removeButton();
            dirty = false;
            cleanedInstance = Instantiate(cleaned, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
            GetComponent<SpriteRenderer>().sprite = cleanSprite;
            GameObject.Find("Player").GetComponent<UIController>().changeCleanliness();
            aim.notifyCleanedUp(gameObject);
            StartCoroutine(turnOffCleaned());
        }
    }

    public IEnumerator turnOffCleaned()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(cleanedInstance);
    }
    
    void removeButton()
    {
        pulsating = false;
        inTrigger = false;
        cleaning = false;
        timeLeft = cleaningTime;
        Destroy(buttonInstance);
    }
}

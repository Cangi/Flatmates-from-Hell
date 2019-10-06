using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;                //Floating point variable to store the player's movement speed.
    private bool movingUp, movingDown, movingLeft, movingRight;
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private Transform playerT;
    private Animator anim;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerT = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        checkMovement();
        
        updateMovement();

        updateAnimations();
    }

    void checkMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            falsifyAll();
            movingUp = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            falsifyAll();
            movingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            falsifyAll();
            movingDown = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            falsifyAll();
            movingLeft = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            movingUp = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            movingRight = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            movingDown = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            movingLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void falsifyAll()
    {
        movingUp = false;
        movingLeft = false;
        movingRight = false;
        movingDown = false;
    }

    void updateMovement()
    {
        if (movingRight)
        {
            playerT.position = new Vector2(playerT.position.x + speed * Time.deltaTime, playerT.position.y);
        }
        if (movingLeft)
        {
            playerT.position = new Vector2(playerT.position.x - speed * Time.deltaTime, playerT.position.y);
        }
        if (movingDown)
        {
            playerT.position = new Vector2(playerT.position.x, playerT.position.y - speed * Time.deltaTime);
        }
        if (movingUp)
        {
            playerT.position = new Vector2(playerT.position.x, playerT.position.y + speed * Time.deltaTime);
        }
    }

    void updateAnimations()
    {
        anim.SetBool("movingRight", movingRight);
        anim.SetBool("movingLeft", movingLeft);
        anim.SetBool("movingDown", movingDown); 
        anim.SetBool("movingUp", movingUp);
    }

}

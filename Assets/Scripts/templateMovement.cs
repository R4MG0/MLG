using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BasMov2 : MonoBehaviour
{
    public float speed = 150f; //speed of the player
    public Animator animator; //animator of the player
    public Sprite rightSprite; //sprites of the player
    public Sprite leftSprite;
    public Sprite straightSprite;
    public Rigidbody2D Virus1;
    public Rigidbody2D Virus2;
    public Rigidbody2D Virus3;
    private float moveHorizontal; //horizontal movement of the player
    public float boundaryTop = 4.5f; //boundaries of the player
    public float boundaryBottom = -4.5f;
    public float boundaryLeft = -8f;
    public float boundaryRight = 80f;
    public TextMeshProUGUI HeartValueTextholder; // Reference to the TextMeshPro component
    public int healthValue = 10; // The health value of the player

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //get the rigidbody of the player
        animator= gameObject.GetComponent<Animator>(); //get the animator of the player
        spriteRenderer = GetComponent<SpriteRenderer>(); //get the sprite renderer of the player
    }

    // Update is called once per frame
    void Update()
    {

        moveHorizontal = Input.GetAxisRaw("Horizontal"); //get the horizontal movement of the player

        if (Input.GetKey(KeyCode.LeftArrow)) //if the player presses the left arrow key
        {
            spriteRenderer.sprite = leftSprite;
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            animator.SetBool("Right", false);
            animator.SetBool("Left", true);
                         //boundaries
            if (transform.position.x < boundaryLeft) //if the player is at the left boundary
            {
                transform.position = new Vector2(boundaryLeft, transform.position.y); //set the player's position to the left boundary
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.sprite = rightSprite;
            rb.velocity = new Vector2(speed, rb.velocity.y);
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
                        //boundaries
            if (transform.position.x > boundaryRight)
            {
                transform.position = new Vector2(boundaryRight, transform.position.y);
            }
        }
        else
        {
            spriteRenderer.sprite = straightSprite;
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }

    }
    void FixedUpdate()
    {
   // rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);    }
}
void OnCollisionEnter2D(Collision2D collision)
{
Debug.Log("Collision detected with " + collision.gameObject.name);
    // Check if the collision is with a specific object
    if (collision.gameObject.CompareTag("Virus") && healthValue > 0)
    {
        // The object has been hit by the other object
        this.healthValue -= 1; // Decrease the health value by 1
        Debug.Log("Health value: " + this.healthValue); // Log the health value
        HeartValueTextholder.text = this.healthValue.ToString(); // Update the health value text
    }
          if(this.healthValue == 0)
      {
            UnityEditor.EditorApplication.isPlaying = false;
        }
}
}

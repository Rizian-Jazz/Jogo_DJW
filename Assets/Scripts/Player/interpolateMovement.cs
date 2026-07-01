using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class interpolateMovement : MonoBehaviour
{
    public float speed = 6f, acceleration = 10f, deceleration = 5f;
    public Rigidbody2D rb;
    private Vector2 playerVelocity, moveInput;

    public Animator anim;
    public bool isFlipped;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(anim == null)
        {
            Debug.LogError("Animator component not found on the player object.");
        }
    }

    void FixedUpdate()
    {
        playerVelocity = (moveInput.normalized * speed);
        Move();
    }
    
     public void onMove(InputAction.CallbackContext context)
    {
          moveInput = context.ReadValue<Vector2>();
    }
    public void Move()
        {

            
            Animations();

            float rate;
            {
                if (playerVelocity.magnitude > 0) 
                {
                    rate = acceleration;
                }
                else
                {
                    rate = deceleration;
                }
            }

            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, playerVelocity, rate * Time.fixedDeltaTime); 


            if (moveInput.x > 0 && !isFlipped)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                isFlipped = true;
            }
            else if (moveInput.x < 0 && isFlipped)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                isFlipped = false;
            }
        }

    public void Animations()
    {
        if (moveInput.x != 0)
            {
                anim.SetBool("isWalkingToSide", true);
            }
            else
            {
                anim.SetBool("isWalkingToSide", false);
            }

            if (moveInput.y > 0)
            {
                anim.SetBool("isWalkingUp", true);
            }
            else
            {
                anim.SetBool("isWalkingUp", false);
            }
            if (moveInput.y < 0)
            {
                anim.SetBool("isWalkingDown", true);
            }
            else
            {
                anim.SetBool("isWalkingDown", false);
            }

    }
}

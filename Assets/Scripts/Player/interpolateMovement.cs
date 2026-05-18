using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class interpolateMovement : MonoBehaviour
{
    public float speed = 6f, acceleration = 10f, deceleration = 5f;
    public Rigidbody2D rb;
    private Vector2 playerVelocity, moveInput;


    void Update()
    {
        playerVelocity = (moveInput.normalized * speed);
        
    }

    void FixedUpdate()
    {
        Move();
       
    }
    
     public void onMove(InputAction.CallbackContext context)
    {
          moveInput = context.ReadValue<Vector2>();
    }
    public void Move()
        {
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
        }

  
}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class interpolateMovement : MonoBehaviour
{
    public float speed = 6f, acceleration = 10f, deceleration = 5f;
    public Rigidbody2D rb;
    private Vector2 targetVelocity, moveInput;


    void Update()
    {
        targetVelocity = (moveInput.normalized * speed);
        
    }

    void FixedUpdate()
    {
        Move();
       
    }
    public void Move()
    {

        
        float rate;
        {
            if (targetVelocity.magnitude > 0) //magnitude é a distância do vetor até a origem, ou seja, o comprimento do vetor (riza)
            {
                rate = acceleration;
            }
            else
            {
                rate = deceleration;
            }
        }

        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, rate * Time.fixedDeltaTime); 
    }
     public void onMove(InputAction.CallbackContext context)
    {
          moveInput = context.ReadValue<Vector2>();
    }


  
}

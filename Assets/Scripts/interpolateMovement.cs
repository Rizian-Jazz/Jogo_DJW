using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class interpolateMovement : MonoBehaviour
{
    public float speed = 6f, acceleration = 10f, deceleration = 5f, bulletInterval = 0.7f;
    public Rigidbody2D rb;
    private float horizontalMovement, verticalMovement;
    private Vector2 targetVelocity, moveInput;

    public bool canThrow = true;

    void Update()
    {
        targetVelocity = (moveInput.normalized * speed);
        
    }

    void FixedUpdate()
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

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }

    public IEnumerator Throw()
    {
        canThrow = false;
        yield return new WaitForSeconds(bulletInterval);
        canThrow = true;
        }

  
}

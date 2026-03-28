using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class interpolateMovement : MonoBehaviour
{


    public float speed = 6f;
    public float acceleration = 10f;
    public float deceleration = 5f;
    public Rigidbody2D rb;
    private float horizontalMovement;
    private float verticalMovement;
    private Vector2 targetVelocity;
    private Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = (moveInput.normalized * speed);
        
    }

    void FixedUpdate()
    {
        
        float rate;
        {
            if (targetVelocity.magnitude > 0)
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
}

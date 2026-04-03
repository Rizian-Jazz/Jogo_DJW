<<<<<<< Updated upstream
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
=======
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
>>>>>>> Stashed changes

public class interpolateMovement : MonoBehaviour
{


<<<<<<< Updated upstream
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
=======
    public float speed = 6f, acceleration = 10f, deceleration = 5f;
    public Rigidbody2D rb;
    private float horizontalMovement, verticalMovement;
    private Vector2 targetVelocity, moveInput;
    public Vector2 lastInput;

    public bool canThrow = true;

>>>>>>> Stashed changes
    void Update()
    {
        targetVelocity = (moveInput.normalized * speed);
        
    }

    void FixedUpdate()
    {
        
        float rate;
        {
<<<<<<< Updated upstream
            if (targetVelocity.magnitude > 0)
=======
            if (targetVelocity.magnitude > 0) //magnitude é a distância do vetor até a origem, ou seja, o comprimento do vetor (riza)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    }
=======
        if (moveInput.magnitude > 0) 
        {
            lastInput = moveInput;  
        }

    }

    public IEnumerator Throw()
    {
        canThrow = false;
        yield return new WaitForSeconds(0.5f);
        canThrow = true;
        }

  
>>>>>>> Stashed changes
}

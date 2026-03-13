using UnityEngine;

public class interpolateMovement : MonoBehaviour
{


    public float speed = 6f;
    public float acceleration = 10f;
    public float deceleration = 5f;
    public Rigidbody2D rb;
    private float horizontalMovement;
    private float verticalMovement;
    private Vector2 targetVelocity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        targetVelocity = new Vector2(horizontalMovement * speed, verticalMovement * speed).normalized * speed;
        
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
}

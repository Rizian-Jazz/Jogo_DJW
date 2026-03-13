using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

using Vector2 = UnityEngine.Vector2;



public class PlayerMovement : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Transform playerTransform;
    
    [Header ("Player Movement")]
    public float speed = 5.0f;
    float horizontalMovement;
    float verticalMovement;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * speed, verticalMovement * speed).normalized * speed;
    }

    public void OnMove(InputValue value)
    {
        Vector2 movementVector = value.Get<Vector2>();
        horizontalMovement = movementVector.x;
        verticalMovement = movementVector.y;
    }
}

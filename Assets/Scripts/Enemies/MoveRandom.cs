using UnityEngine;
using System.Collections;

public class MoveRandom : MonoBehaviour
{
    public float speed = 7f;

    private bool isWandering = false;
    private Vector2 movementDirection;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!isWandering)
        StartCoroutine(Wander());

            if(rb != null)
        {
            rb.linearVelocity = movementDirection * speed;
        }
        
    }
    IEnumerator Wander()
    {
        isWandering = true;
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        float walkTime = Random.Range(1f, 3f);
        yield return new WaitForSeconds(walkTime);

        isWandering = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collider"))
        {
            movementDirection = -movementDirection;
        }
    }
    
}

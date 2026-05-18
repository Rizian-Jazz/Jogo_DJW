using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnHealth = 100;
    public int EnDamage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Enemy hit by bullet! -20 HP");
            EnHealth -= 20;
            if(EnHealth <= 0)
            {
                Destroy(gameObject);
            }
        }  
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(EnDamage);
                Debug.Log("Enemy collided with player! Player takes " + EnDamage + " damage.");
            }
        }
    }
}

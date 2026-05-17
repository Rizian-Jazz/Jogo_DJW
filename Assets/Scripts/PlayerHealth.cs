using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Player HP: " + health);
        if (health <= 0)
        {
            Debug.Log("Player morreu");
            Destroy(gameObject);
        }
    }
    public void Heal(float amount)
    {
        health += amount;
        if (health > 100f) health = 100f;
        Debug.Log("Player HP: " + health);
    }
}

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5, currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    public void TakeDamage(int amount)
    {
        currentHealth -= (int)amount;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Player HP: " + currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Player morreu");
            Destroy(gameObject);
        }
    }
    public void Heal(int amount)
    {
        currentHealth += (int)amount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log("Player HP: " + currentHealth);
    }
}

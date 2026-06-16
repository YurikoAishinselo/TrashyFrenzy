using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Optional")]
    public bool destroyOnDeath = false;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        Debug.Log(gameObject.name + " took damage: " + damage);
        Debug.Log("Current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log(gameObject.name + " died");

        // If this is the player
        PlayerController player = GetComponent<PlayerController>();
        if (player != null)
        {
            player.inputLocked = true;
        }

        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
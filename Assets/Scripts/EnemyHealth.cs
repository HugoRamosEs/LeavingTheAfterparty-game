using UnityEngine;

/// <summary>
/// This script is responsible for the enemy health and damage.
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    private int maxHealth;

    public int health;
    public GameObject bloodParticles;
    public HealthBar healthBar;
    public delegate void HealthChanged(int currentHealth);
    public static event HealthChanged OnHealthChanged;

    /// <summary>
    /// Stablishes the max health and updates the health bar.
    /// </summary>
    private void Start()
    {
        maxHealth = health;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    /// <summary>
    /// Takes damage and updates the health bar.
    /// </summary>
    /// <param name="damage"> Damage taken</param>
    public void TakeDamage(int damage)
    {
        if (!BarcoBossFight.invulnerable)
        {
            health -= damage;
            Debug.Log(health);

            healthBar.UpdateHealthBar(health, maxHealth);

            OnHealthChanged?.Invoke(health);

            if (bloodParticles != null)
            {
                Instantiate(bloodParticles, transform.position, Quaternion.identity);
            }
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
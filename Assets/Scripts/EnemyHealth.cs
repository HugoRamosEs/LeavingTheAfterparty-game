using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public GameObject bloodParticles;
    public HealthBar healthBar;
    public delegate void HealthChanged(int currentHealth);
    public static event HealthChanged OnHealthChanged;

    private int maxHealth;

    private void Start()
    {
        maxHealth = health;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

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
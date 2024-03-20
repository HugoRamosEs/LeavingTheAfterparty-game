using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public GameObject bloodParticles;
    public HealthBar healthBar;

    private int maxHealth;

    private void Start()
    {
        maxHealth = health;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);

        healthBar.UpdateHealthBar(health, maxHealth);

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
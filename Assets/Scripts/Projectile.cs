using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for the behavior of the projectile.
/// </summary>
public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage = 10f;

    public GameObject destructionParticles;

    private Transform player;
    private Vector2 initialPosition;
    private Vector2 target;

    /// <summary>
    /// This method is used to set the initial position of the projectile and the target position.
    /// </summary>
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position;
        Vector2 directionToPlayer = (Vector2)player.position - (Vector2)initialPosition;
        target = initialPosition + 2 * directionToPlayer;

        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        angle -= 45;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    /// <summary>
    /// This method is used to move the projectile towards the target position.
    /// </summary>
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if ((Vector2)transform.position == target)
        {
            DestroyProjectile();
        }
    }

    /// <summary>
    /// Th method is used to detect the collision with the player and deal damage to it.
    /// </summary>
    /// <param name="collision"> Player's collision</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
            }
            DestroyProjectile();
        }
    }

    /// <summary>
    /// This method is used to instantiate the destruction particles and destroy the projectile.
    /// </summary>
    void DestroyProjectile()
    {
        Instantiate(destructionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
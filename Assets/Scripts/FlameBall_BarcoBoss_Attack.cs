using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This method is used to control the FlameBall attack from the BarcoBoss.
/// </summary>
public class FlameBall_BarcoBoss_Attack : MonoBehaviour
{
    public float speed;
    public float damage = 10f;
    public float destroyThreshold = 500f;

    public GameObject destructionParticles;

    private Vector2 direction;
    private float distanceTravelled = 0f;

    /// <summary>
    /// This method is used to set the direction of the projectile.
    /// </summary>
    private void Start()
    {
        direction = new Vector2(0, -1);
    }

    /// <summary>
    /// This method is used to move the projectile and destroy it when it reaches the destroyThreshold.
    /// </summary>
    private void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.position = (Vector2)transform.position + direction * moveDistance;
        distanceTravelled += moveDistance;

        if (distanceTravelled > destroyThreshold)
        {
            DestroyProjectile();
        }
    }

    /// <summary>
    ///  This method is used to detect when the projectile collides with the player and deal damage to it.
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
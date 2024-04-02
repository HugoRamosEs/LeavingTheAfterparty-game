using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBall_BarcoBoss_Attack : MonoBehaviour
{
    public float speed;
    public float damage = 10f;
    public float destroyThreshold = 500f;

    public GameObject destructionParticles;

    private Vector2 direction;
    private float distanceTravelled = 0f;

    private void Start()
    {
        direction = new Vector2(0, -1);
    }

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

    void DestroyProjectile()
    {
        Instantiate(destructionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
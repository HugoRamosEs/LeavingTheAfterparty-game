using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heiser : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
            }
        }
    }

    public void OnAnimationComplete()
    {
        Destroy(gameObject);
    }
}
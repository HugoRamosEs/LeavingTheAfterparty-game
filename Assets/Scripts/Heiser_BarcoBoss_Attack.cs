// At this moment, this script is not used, but it is a script that is going to be used in the future for another BarcoBoss attack.

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
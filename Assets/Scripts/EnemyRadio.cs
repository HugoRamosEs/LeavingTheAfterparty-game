using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadio : MonoBehaviour
{
    public float detectionRadius;
    private Transform player;

    private EnemyShooting enemyShooting;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyShooting = GetComponent<EnemyShooting>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (enemyShooting != null)
            {
                enemyShooting.enabled = true;
            }
        }
        else
        {
            if (enemyShooting != null)
            {
                enemyShooting.enabled = false;
            }
        }
    }
}

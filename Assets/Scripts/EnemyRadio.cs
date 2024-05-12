using UnityEngine;
using UnityEngine.AI;

public class EnemyRadio : MonoBehaviour
{
    private Transform player;
    private EnemyShooting enemyShooting;
    private NavMeshAgent agent;

    public float detectionRadius;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyShooting = GetComponent<EnemyShooting>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);

        if (distanceToPlayer <= detectionRadius && !enemyShooting.isPreparingToShoot)
        {
            enemyShooting.enabled = true;
            agent.isStopped = false;
        }
        else
        {
            enemyShooting.enabled = false;
            agent.isStopped = true;
        }
    }
}

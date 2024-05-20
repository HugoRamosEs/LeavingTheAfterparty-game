using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// This class stablishes the enemy's radio communication with the player.
/// </summary>
public class EnemyRadio : MonoBehaviour
{
    private Transform player;
    private EnemyShooting enemyShooting;
    private NavMeshAgent agent;

    public float detectionRadius;

    /// <summary>
    /// This method is used to check for the player and stablish the enemy's shooting and navigation components.
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyShooting = GetComponent<EnemyShooting>();
        agent = GetComponent<NavMeshAgent>();
    }
    /// <summary>
    /// This method is used to check the distance between the player and the enemy, and enable or disable the enemy's shooting and navigation components.
    /// </summary>
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

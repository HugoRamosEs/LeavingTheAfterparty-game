using System.Collections;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// This script is responsible for the enemy's shooting behavior.
/// </summary>
public class EnemyShooting : MonoBehaviour
{
    private float timeBtwShots;
    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float shootingDistance;
    public float startTimeBtwShots;
    public bool isPreparingToShoot = false;
    public GameObject projectile;

    /// <summary>
    /// This method is used to initialize the enemy's shooting behavior.
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
    }

    /// <summary>
    /// This method is used to update the enemy's shooting behavior.
    /// </summary>
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);

        if (timeBtwShots <= 0 && !isPreparingToShoot && distanceToPlayer <= shootingDistance)
        {
            StartCoroutine(PrepareToShoot());
        }
        else if (!isPreparingToShoot)
        {
            timeBtwShots -= Time.deltaTime;
            animator.SetBool("isShooting", false);

            if (distanceToPlayer > stoppingDistance)
            {
                
                agent.isStopped = false;
                agent.SetDestination(player.position);
                animator.SetBool("isMoving", true);
                animator.SetFloat("x", player.position.x - agent.transform.position.x);
                animator.SetFloat("y", player.position.y - agent.transform.position.y);
            }
            else if (distanceToPlayer < stoppingDistance && distanceToPlayer > retreatDistance)
            {
                agent.isStopped = true;
                animator.SetBool("isMoving", false);
            }
            else if (distanceToPlayer <= retreatDistance)
            {
                Vector3 dirToPlayer = (player.position - agent.transform.position).normalized;
                Vector3 newPos = agent.transform.position - dirToPlayer * retreatDistance;
                agent.SetDestination(newPos);
                animator.SetBool("isMoving", true);
                animator.SetFloat("x", player.position.x - agent.transform.position.x);
                animator.SetFloat("y", player.position.y - agent.transform.position.y);
            }
        }
    }

    /// <summary>
    /// This method is used to prepare the enemy to shoot.
    /// </summary>
    /// <returns></returns>
    IEnumerator PrepareToShoot()
    {
        isPreparingToShoot = true;
        animator.SetBool("isMoving", false);
        animator.SetBool("isShooting", true);

        agent.isStopped = true;

        yield return new WaitForSeconds(1);

        Instantiate(projectile, agent.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        timeBtwShots = startTimeBtwShots;
        isPreparingToShoot = false;

        agent.isStopped = false;
    }
}

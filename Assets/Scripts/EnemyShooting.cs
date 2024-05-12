using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
                // Moverse hacia el jugador usando NavMesh
                agent.isStopped = false;
                agent.SetDestination(player.position);
                animator.SetBool("isMoving", true);
                // Añade las siguientes líneas para actualizar las variables de dirección
                animator.SetFloat("x", player.position.x - agent.transform.position.x);
                animator.SetFloat("y", player.position.y - agent.transform.position.y);
            }
            else if (distanceToPlayer < stoppingDistance && distanceToPlayer > retreatDistance)
            {
                // Detenerse a una distancia prudente del jugador
                agent.isStopped = true;
                animator.SetBool("isMoving", false);
            }
            else if (distanceToPlayer <= retreatDistance)
            {
                // Retroceder si está demasiado cerca del jugador
                Vector3 dirToPlayer = (player.position - agent.transform.position).normalized;
                Vector3 newPos = agent.transform.position - dirToPlayer * retreatDistance;
                agent.SetDestination(newPos);
                animator.SetBool("isMoving", true);
                // Añade las siguientes líneas para actualizar las variables de dirección
                animator.SetFloat("x", player.position.x - agent.transform.position.x);
                animator.SetFloat("y", player.position.y - agent.transform.position.y);
            }
        }
    }

    IEnumerator PrepareToShoot()
    {
        isPreparingToShoot = true;
        animator.SetBool("isMoving", false);
        animator.SetBool("isShooting", true);

        // Detener el agente de navegación mientras se prepara para disparar
        agent.isStopped = true;

        yield return new WaitForSeconds(1);

        Instantiate(projectile, agent.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        // Reiniciar el tiempo entre disparos y permitir que el agente se mueva nuevamente
        timeBtwShots = startTimeBtwShots;
        isPreparingToShoot = false;

        // Permitir que el agente se mueva después de disparar
        agent.isStopped = false;
    }
}

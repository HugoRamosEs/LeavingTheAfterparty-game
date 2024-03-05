using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float shootingDistance;
    private bool isPreparingToShoot = false;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    private Transform player;

    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (timeBtwShots <= 0 && !isPreparingToShoot && distanceToPlayer <= shootingDistance)
        {
            StartCoroutine(PrepareToShoot());
        }
        else if (!isPreparingToShoot)
        {
            timeBtwShots -= Time.deltaTime;
            animator.SetBool("isShooting", false);

            float stoppingDistanceTolerance = 0.1f;

            if (distanceToPlayer > stoppingDistance + stoppingDistanceTolerance)
            {
                // Moverse hacia el jugador
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                animator.SetBool("isMoving", true);
                animator.SetFloat("x", player.position.x - transform.position.x);
                animator.SetFloat("y", player.position.y - transform.position.y);
            }
            else if (distanceToPlayer < stoppingDistance - stoppingDistanceTolerance && distanceToPlayer > retreatDistance)
            {
                // Estar en una posición prudente
                animator.SetBool("isMoving", false);
            }
            else if (distanceToPlayer <= retreatDistance)
            {
                // Estar demasiado cerca del jugador
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                animator.SetBool("isMoving", true);
                animator.SetFloat("x", player.position.x - transform.position.x);
                animator.SetFloat("y", player.position.y - transform.position.y);
            }
        }
    }

    IEnumerator PrepareToShoot()
    {
        isPreparingToShoot = true;
        animator.SetBool("isMoving", false);
        animator.SetBool("isShooting", true);

        yield return new WaitForSeconds(1);

        Instantiate(projectile, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        timeBtwShots = startTimeBtwShots;
        isPreparingToShoot = false;
    }
}
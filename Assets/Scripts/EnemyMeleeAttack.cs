using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for the enemy melee attack.
/// </summary>
public class EnemyMeleeAttack : MonoBehaviour
{
    private Transform target;
    public float speed;
    public float attackDistance = 1f;
    public float damage = 10f;

    private Animator animator;
    private bool isFacingRight = true;
    private bool attacking = false;

    /// <summary>
    /// Check for the player as a target.
    /// </summary>
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Move the enemy towards the player and attack when in range.
    /// </summary>
    void Update()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, target.position) > attackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            attacking = false;
        }
        else if (Vector2.Distance(transform.position, target.position) <= attackDistance)
        {
            attacking = true;
        }

        float vertical = direction.y;
        float horizontal = direction.x;

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            bool isPlayerRight = horizontal > 0;
            Flip(isPlayerRight);
            animator.SetFloat("vertical", 0f);
            animator.SetFloat("horizontal", Mathf.Abs(horizontal));
        }
        else
        {
            animator.SetFloat("vertical", vertical);
            animator.SetFloat("horizontal", 0f);
        }

        animator.SetBool("attacking", attacking);
    }

    /// <summary>
    /// Flip the enemy sprite when changing direction.
    /// </summary>
    /// <param name="isPlayerRight"> Check if the player is on the right of the enemy or on the left</param>
    void Flip(bool isPlayerRight)
    {
        if ((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    /// <summary>
    /// Deal damage to the player when in range.
    /// </summary>
    /// <param name="collision"> Player's collision</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && attacking)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
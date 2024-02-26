using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuliamMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;
    private bool attacking;
    private bool isFacingRight = true;
    private bool isWaitingAfterDamage = false;

    [SerializeField] private float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isWaitingAfterDamage = false;
    }
    void Update()
    {
        if (player == null)
        {
            CheckForPlayerWithTag();
        }

        if (attacking)
        {
            animator.SetBool("attacking", true);
        }
        else
        {
            animator.SetBool("attacking", false);
        }

        if (!isWaitingAfterDamage && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;

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
        }
    }
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player p = other.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(1500f);
                StartCoroutine(DamagePlayer());
            }
        }
    }
    IEnumerator DamagePlayer()
    {
        isWaitingAfterDamage = true;
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        attacking = true;
        yield return new WaitForSeconds(0.75f);
        isWaitingAfterDamage = false;
        attacking = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void CheckForPlayerWithTag()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
}

using System.Collections;
using UnityEngine;

public class Borracho : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;
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
        yield return new WaitForSeconds(0.75f);
        isWaitingAfterDamage = false;
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

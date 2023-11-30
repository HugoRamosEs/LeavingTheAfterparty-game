using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    public Vector2 lastMoveInput;
    private Animator animator;
    private bool moving;
    private Player player;
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(
            horizontal,
            vertical
            );
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);

        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving);

        if (horizontal != 0 || vertical != 0)
        {
            lastMoveInput = new Vector2(
                horizontal,
                vertical
                ).normalized;
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }
    }
    void FixedUpdate()
    {
        Move();
        if (moving)
        {
            player.GetTired(1);
        }
        else
        {
            player.Rest(1);
        }
    }
    private void Move()
    {
        playerRb.velocity = moveInput * speed;
    }
}

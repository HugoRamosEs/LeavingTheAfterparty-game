using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float sprintSpeed = 4f;

    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Vector2 lastMoveInput;
    private Animator animator;
    private bool moving;
    private bool sprinting;
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
        moveInput = new Vector2(horizontal, vertical);
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
        bool wasSprinting = sprinting;
        sprinting = Input.GetKey(KeyCode.LeftShift) && player.stamina.currentVal > 0;
        if (sprinting)
        {
            Sprint();
        }
        else
        {
            Move();
        }
        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving);

        if (horizontal != 0 || vertical != 0)
        {
            lastMoveInput = new Vector2(horizontal, vertical).normalized;
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }
        float currentSpeed = sprinting ? 1.5f : 1f;
        animator.SetFloat("sprintSpeed", currentSpeed);
    }
    private void Move()
    {
        playerRb.velocity = moveInput.normalized * speed;
        player.Rest(20f);
    }
    private void Sprint()
    {
        playerRb.velocity = moveInput.normalized * sprintSpeed;
        player.GetTired(30f);  
    }
}

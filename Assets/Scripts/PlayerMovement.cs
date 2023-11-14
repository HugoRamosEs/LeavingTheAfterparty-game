using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator animator;
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            );
        animator.SetFloat("horizontal", moveInput.x);
        animator.SetFloat("vertical", moveInput.y);
    }
    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        playerRb.velocity = moveInput * speed;
    }
}

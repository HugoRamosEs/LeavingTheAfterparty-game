using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Player player;

    private Animator anim;
    public float moveSpeed;
    public float sprintSpeed;

    private float currentMoveSpeed;

    public float x, y;
    private bool isWalking;

    private Vector3 moveDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Start()
    {
        currentMoveSpeed = moveSpeed;
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (x != 0 || y != 0)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
            if (!isWalking)
            {
                isWalking = true;
                anim.SetBool("isMoving", isWalking);
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                anim.SetBool("isMoving", isWalking);
                StopMoving();
            }
        }

        moveDir = new Vector3(x, y).normalized;

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && player.stamina.currentVal > 0)
        {
            currentMoveSpeed = sprintSpeed;
            player.GetTired(30f);
        }
        else
        {
            currentMoveSpeed = moveSpeed;
            StartCoroutine(DelayedRest(20f));
        }
    }

    IEnumerator DelayedRest(float restTime)
    {
        yield return new WaitForSeconds(2f);
        player.Rest(restTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * currentMoveSpeed * Time.deltaTime;
    }

    void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }
}

using System.Collections;
using UnityEngine;
/// <summary>
/// This script is used to control the player movement
/// </summary>
public class Player2Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Player player;

    private Animator anim;
    public float moveSpeed;
    public float sprintSpeed;

    private float currentMoveSpeed;

    private float x, y;
    private bool isWalking;
    private bool isSprinting;
    public bool isResting;

    private Vector3 moveDir;

    public PlayerAttackShooting playerAttackShooting;
    public PlayerAttackMelee playerAttackMelee;

    /// <summary>
    /// This method is uses to ensure the functionality of the player movement
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    /// <summary>
    /// This method is used to set the current move speed of the player
    /// </summary>
    void Start()
    {
        currentMoveSpeed = moveSpeed;
    }

    /// <summary>
    /// This method is used to update the player movement and animation at each frame
    /// </summary>
    void Update()
    {
        if (Time.deltaTime == 0)
            return;

        if (playerAttackShooting.isShooting || playerAttackMelee.isAttacking)
        {
            StopMoving();
            return;
        }

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

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (player.stamina.currentVal > 0) && !isResting && isWalking)
        {
            currentMoveSpeed = sprintSpeed;
            player.GetTired(30f);
            isSprinting = true;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
            if (isSprinting)
            {
                isSprinting = false;
                if (player.stamina.currentVal <= 0)
                {
                    StartCoroutine(DelayedRest());
                }
            }
            else
            {
                if (!isSprinting && !isResting)
                {
                    player.Rest(20f);
                }
            }
        }
    }


    /// <summary>
    /// This method is used to make the player rest after sprinting
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayedRest()
    {
        isResting = true;
        yield return new WaitForSeconds(2f);
        player.Rest(20f);
        isResting = false;
    }

    /// <summary>
    /// This method is used to fix and ensure the player movement
    /// </summary>
    private void FixedUpdate()
    {
        if (Time.deltaTime == 0)
            return;

        if (playerAttackShooting.isShooting || playerAttackMelee.isAttacking)
        {
            StopMoving();
        }
        else
        {
            rb.velocity = moveDir * currentMoveSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// This method is used to stop the player movement
    /// </summary>
    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }
}
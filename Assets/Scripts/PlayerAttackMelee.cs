using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask enemyDamageLayer;
    public float attackRange;
    public int damage;

    private Animator anim;
    public bool isAttacking = false;

    public ItemPanel itemPanel;
    public ToolBar toolbar;
    public string attackItem;

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            for (int i = 0; i < itemPanel.inventory.slots.Count; i++)
            {
                ItemSlot slot = itemPanel.inventory.slots[i];
                if (i == toolbar.selectedTool && slot.item != null && slot.item.Name == attackItem)
                {
                    if (Input.GetMouseButtonDown(0) && !isAttacking)
                    {
                        isAttacking = true;
                        anim.SetBool("isMelee", true);

                        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyDamageLayer);
                        foreach (Collider2D collider in colliders)
                        {
                            if (collider is CapsuleCollider2D)
                            {
                                EnemyHealth enemyHealth = collider.GetComponentInParent<EnemyHealth>();
                                if (enemyHealth != null)
                                {
                                    enemyHealth.TakeDamage(damage);
                                }
                            }
                        }
                        timeBtwAttack = startTimeBtwAttack;
                        StartCoroutine(StopAttackAnimation());
                    }
                }
            }
           
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    IEnumerator StopAttackAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isMelee", false);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
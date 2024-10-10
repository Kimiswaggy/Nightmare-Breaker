using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Melee : Enemy_Base
{
    protected override void Start()
    {
        enemyMaxHealth = 5;
        detectionRange = 5f;
        stopChaseRange = 8f;
        attackRange = 1.5f;
        damageAmount = 1;
        moveSpeed = 3f;

        base.Start();
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange && !isAttacking)
        {
            ChasePlayer();
        }

        else if (distanceToPlayer <= attackRange && !isAttacking)
        {
            StartCoroutine(AttackPlayer());
        }

        else if (distanceToPlayer > stopChaseRange)
        {
            StopChase();
        }
    }

    protected override IEnumerator AttackPlayer()
    {
        isAttacking = true;
        animator.SetBool("isMoving", false);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D player in hitPlayer)
        {
            if (player.CompareTag("Player"))
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    Debug.Log("Player take damage");
                    playerHealth.TakeDamage(damageAmount);
                }
            }
        }

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length-0.5f);

        isAttacking = false;

        yield return new WaitForSeconds(1f);

        }
    }
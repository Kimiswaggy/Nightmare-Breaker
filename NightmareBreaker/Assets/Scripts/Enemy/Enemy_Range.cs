using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Range : Enemy_Base
{
    [SerializeField] private GameObject magicProjectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private float attackCoolDown = 2f;
    private bool canShoot = true;

    protected override void Start()
    {
        enemyMaxHealth = 4;
        detectionRange = 6f;
        stopChaseRange = 8f;
        attackRange = 4f;
        damageAmount = 2;
        moveSpeed = 2f;

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

        else if (distanceToPlayer <= attackRange && !isAttacking && canShoot)
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
        canShoot = false; //prevent attacking again during CD
        animator.SetBool("isMoving", false);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);

        GameObject projectile = Instantiate(magicProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Vector2 direction = (player.position - projectileSpawnPoint.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.5f);
        
        isAttacking = false;//Allow movement again
        yield return new WaitForSeconds(attackCoolDown);
        canShoot = true;
    }
}

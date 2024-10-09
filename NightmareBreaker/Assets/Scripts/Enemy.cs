using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyCurrentHealth, enemyMaxHealth = 5;

    public float detectionRange = 5f;
    public float stopChaseRange = 8f;
    public float attackRange = 1.5f;
    public int damageAmount = 1;
    public float moveSpeed = 3f;

    private Transform player;
    private bool isAttacking = false;
    private bool canDealDamage = false;
    private bool isDead = false;
    private Animator animator;

    private bool facingRight = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        enemyCurrentHealth = enemyMaxHealth;
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

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        enemyCurrentHealth -= damage;
        animator.SetTrigger ("isHurt");
        if (enemyCurrentHealth <= 0)
        {
            enemyCurrentHealth = 0;
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        isDead = true;
        animator.SetTrigger("isDead");
        StopChase();
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    
    private void ChasePlayer()
    {
        animator.SetBool("isMoving", true);
        Vector2 direction = (player.position - transform.position).normalized;

        if (direction.x < 0 && facingRight)
        {
            Flip();
        }
        else if (direction.x > 0 && !facingRight)
        {
            Flip();
        }

        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

    }

    //Flip the enemy sprite direction to its moving direction
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void StopChase()
    {
        animator.SetBool("isMoving", false);
    }

    private IEnumerator AttackPlayer()
    {
        Debug.Log("Enemy Attack");
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
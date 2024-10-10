using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Base : MonoBehaviour
{
    [SerializeField] protected int enemyMaxHealth;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float stopChaseRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected int damageAmount;
    [SerializeField] protected float moveSpeed;

    protected int enemyCurrentHealth;
    protected Transform player;
    protected bool isAttacking = false;
    protected bool canDealDamage = false;
    protected bool isDead = false;
    protected Animator animator;
    protected bool facingRight = true;

    protected virtual void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        enemyCurrentHealth -= damage;
        animator.SetTrigger("isHurt");

        if (enemyCurrentHealth <= 0)
        {
            enemyCurrentHealth = 0;
            TriggerDeath();
        }
    }
    protected virtual void TriggerDeath()
    {
        isDead = true;
        animator.SetTrigger("isDead");
        StopChase();
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    protected void ChasePlayer()
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
    protected void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected void StopChase()
    {
        animator.SetBool("isMoving", false);
    }

    protected abstract IEnumerator AttackPlayer();
}

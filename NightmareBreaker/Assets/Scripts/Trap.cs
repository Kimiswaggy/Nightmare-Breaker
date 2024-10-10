using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 2;
    public float detectRange = 6f;
    public float attackCooldown = 2f;

    private bool isAttacking = false;
    private bool canAttack = true;
    private Transform player;
    private Animator animator;
    private Collider2D spikeCollider;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spikeCollider = GetComponentInChildren<Collider2D>();
        spikeCollider.enabled = false;//disable it by default
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectRange && canAttack && !isAttacking)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        isAttacking = true;
        canAttack = false;

        animator.SetTrigger("Attack");
        StartCoroutine(EnableSpikeCollider());
    }

    IEnumerator EnableSpikeCollider()
    {
        yield return new WaitForSeconds(0.5f);
        spikeCollider.enabled = true;
        yield return new WaitForSeconds(1f);
        spikeCollider.enabled = false;
        animator.ResetTrigger("Attack");
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && spikeCollider.enabled)
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}

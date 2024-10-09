using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponController : MonoBehaviour
{
    private enum PlayerState
    {
        NoWeapon,
        Melee,
        Bow
    }

    private PlayerState currentState;
    private bool isAttacking = false;

    private Animator animator;

    [SerializeField] private float meleeAttackRange = 2f;
    [SerializeField] private int meleeDamage = 1;

    [SerializeField] private float bowRange = 10f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float arrowSpeed = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        ChangeState(PlayerState.NoWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState(PlayerState.Melee);
            Debug.Log("State: Melee");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(PlayerState.Bow);
            Debug.Log("State: Bow");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(PlayerState.NoWeapon);
            Debug.Log("State: NoWeapon");
        }

        HandlePlayerAbilities();
    }

    private void ChangeState(PlayerState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case PlayerState.NoWeapon:
                animator.SetBool("isMelee", false);
                animator.SetBool("isBow", false);
                break;

            case PlayerState.Melee:
                animator.SetBool("isMelee", true);
                animator.SetBool("isBow", false);
                break;

            case PlayerState.Bow:
                animator.SetBool("isMelee", false);
                animator.SetBool("isBow", true);
                break;
        }
    }

    private void HandlePlayerAbilities()
    {
        switch (currentState)
        {
            case PlayerState.Melee:
                if (Input.GetMouseButtonDown(0)&& !isAttacking)
                {
                    MeleeAttack();
                }
                break;

            case PlayerState.Bow:
                if (Input.GetMouseButtonDown(0)&& !isAttacking)
                {
                    BowAttack();
                }
                break;
        }
    
    }

    private void MeleeAttack()
    {
        Debug.Log("Sword attack!");
        animator.SetTrigger("isAttacking");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, meleeAttackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Enemy enemyHealth = enemy.GetComponent<Enemy>();
                if (enemyHealth != null) {
                    enemyHealth.TakeDamage(meleeDamage);
            }
        }
    }

        StartCoroutine(AttackCoolDown());

    }

    private void BowAttack()
    {
        Debug.Log("Bow attack!");
        animator.SetTrigger("isShooting");

        if (arrowPrefab != null && arrowSpawnPoint!= null)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

            Vector2 shootDirection = new Vector2(animator.GetFloat("LastHorizontal"), animator.GetFloat("LastVertical")).normalized;
           
            if (shootDirection == Vector2.zero)
            {
                Debug.LogWarning("No shooting direction");
                return;
            }

            //calculate the angle for the arrow rotation
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            //set the arrow rotation to match the shooting direction
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            //set arrow velocity to move the shooting direction
            arrow.GetComponent<Rigidbody2D>().velocity = shootDirection * arrowSpeed;
        }

        else
        {
            Debug.LogWarning("missing arrow prefab");
        }

        StartCoroutine(AttackCoolDown());
    }

    private IEnumerator AttackCoolDown()
    {
        isAttacking = true;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;
    }
}
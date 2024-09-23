using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking) // Attack with sword
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("Sword attack!");
        animator.SetTrigger("isAttacking");
        StartCoroutine(AttackCoolDown());
    }

    private IEnumerator AttackCoolDown()
    {
        isAttacking = true;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;
    }
}

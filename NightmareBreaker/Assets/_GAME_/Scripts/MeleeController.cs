using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Attack with sword
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("Sword attack!");
        animator.SetTrigger("isAttacking");
    }
}

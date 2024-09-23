using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class WeaponController : MonoBehaviour
{
    private enum PlayerState
    {
        NoWeapon,
        Melee,
        Bow
    }

    private PlayerState currentState;

    private Animator animator;

    [SerializeField] private float meleeAttackRange = 2f;
    [SerializeField] private float bowRange = 10f;
    [SerializeField] private GameObject arrowPrefeb;

    void Start()
    {
        animator = GetComponent<Animator>();
        arrowPrefeb = GetComponent<GameObject>();
        ChangeState(PlayerState.NoWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState(PlayerState.Melee);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(PlayerState.Bow);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(PlayerState.NoWeapon);
        }
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
                if (Input.GetMouseButtonDown(0))
                {
                    MeleeAttack();
                }
                break;

            case PlayerState.Bow:
                // Bow attack input (e.g., LMB to shoot)
                if (Input.GetMouseButtonDown(0))
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
    }

    private void BowAttack()
    {
        Debug.Log("Bow attack!");
        animator.SetTrigger("isShooting");

        // Spawn an arrow prefab
        Instantiate(arrowPrefeb, transform.position, Quaternion.identity);

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDashing)
        {
            return; 
        }
        movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        animator.SetFloat(horizontal,movement.x);
        animator.SetFloat (vertical,movement.y);

        if (movement != Vector2.zero)
        {
            animator.SetFloat(lastHorizontal, movement.x);
            animator.SetFloat(lastVertical, movement.y);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) & canDash)
        {
            StartCoroutine(Dash());
        }

    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = movement * moveSpeed;
    }

    private IEnumerator Dash ()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = movement.normalized * dashingPower;
        tr.emitting = true;
        
        yield return new WaitForSeconds(dashingTime);
        
        tr.emitting = false;
        rb.velocity = Vector2.zero;
        isDashing = false;
        
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

}

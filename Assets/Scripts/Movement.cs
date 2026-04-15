using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    public float movementSpeed;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private bool hasNinjaPerk = false;
    private float dashTime;
    private float dashCooldownTimer;
    private Rigidbody2D rb;
    private UnityEngine.Vector2 moveInput;
    private UnityEngine.Vector2 dashDirection;
    private UnityEngine.Vector2 lastMoveDirection;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * movementSpeed;
        if (isDashing)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            dashTime -= Time.deltaTime;

            if(!hasNinjaPerk){
                GetComponent<Collider2D>().enabled = false;
            }

            if (dashTime <= 0)
            {
                isDashing = false;
                GetComponent<Collider2D>().enabled = true;
            }

            return;
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        rb.linearVelocity = moveInput * movementSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isRunning", true);

        if (context.canceled)
        {
            animator.SetBool("isRunning", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        
        moveInput = context.ReadValue<UnityEngine.Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    public void Dash(InputAction.CallbackContext context)
    {

        if (context.started && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;


            if(moveInput != UnityEngine.Vector2.zero)
            {
                dashDirection = moveInput.normalized;
            } else
            {
                dashDirection = lastMoveDirection;
            }
            
        }
    }

    public void ninjaPerk()
    {
        // Reduce dash cooldown significantly but remove invulnerability
        if (!hasNinjaPerk)
        {
            hasNinjaPerk = true;
            dashCooldown = 0.5f;
        }
    }

    public void brutePerk()
    {
        movementSpeed = movementSpeed / 2;
    }
}

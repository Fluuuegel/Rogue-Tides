using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{

    Vector2 moveInput; // 1 and -1 for x and y axis respectively

    Vector2 lookDirection; // Direction the player is looking at

    SpriteRenderer spriteRenderer; // Reference to the sprite renderer for animation
    [SerializeField] private float speed = 5f; // Speed of the player

    Rigidbody2D rb;

    Animator animator;

    PlayerStats playerStats;

    void Start()
    {   
        speed = 5f; // Set the speed of the player
        lookDirection = Vector2.down; // Default look direction

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Update() {
        
        lookDirection = moveInput.normalized;
        if (lookDirection != Vector2.zero) {
            // Set the animation based on the look direction
            animator.SetBool("IsMoving", true);
            animator.SetFloat("LookDirectionX", lookDirection.x);
            animator.SetFloat("LookDirectionY", lookDirection.y);
        } else {
            animator.SetBool("IsMoving", false);
        }

        Vector2 rayOrigin = rb.position + new Vector2(0, -0.1f); // Adjust the ray origin to the player's position
        // Debug.DrawRay(rayOrigin, lookDirection, Color.red);
        rb.linearVelocity = moveInput * speed; // Set the linear velocity of the player
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, lookDirection, 1f, LayerMask.GetMask("NPC"));
            if (hit.collider != null) {
                // Trigger Dialog
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null && interactable.IsInteractable()) {
                    // isDialogActive = true;
                    interactable.Interact();
                }
            }
        }
    }

    public void GetMoveInput(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }
}

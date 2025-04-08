using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{

    Vector2 moveInput; // 1 and -1 for x and y axis respectively

    Vector2 lookDirection; // Direction the player is looking at

    SpriteRenderer spriteRenderer; // Reference to the sprite renderer for animation
    public float speed = 1f; // Speed of the player

    Rigidbody2D rb;

    public ContactFilter2D contactFilter; // Determine where the raycast will check for collisions
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // List to store the collisions

    Animator animator;

    PlayerStats playerStats;

    public float collisionOffset = 0.01f; // Offset for the collision check
    void Start()
    {   
        speed = 1f; // Set the speed of the player
        collisionOffset = 0.01f;
        lookDirection = Vector2.down; // Default look direction

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Update() {
        Vector2 rayOrigin = rb.position + new Vector2(0, -0.1f); // Adjust the ray origin to the player's position
        // Debug.DrawRay(rayOrigin, lookDirection, Color.red);
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

    private void FixedUpdate() {
        bool moved = false;


        // Movement
        if (moveInput != Vector2.zero) { // Tricks for smoother movement
            moved = Move(moveInput); // Try to move in the direction of the input
            if (!moved) { // If not moved in x direction, try y direction
                moved = Move(new Vector2(moveInput.x, 0));
            }
            if (!moved) {
                moved = Move(new Vector2(0, moveInput.y));
            }
        }

        if (moved) {
            animator.SetBool("IsMoving", true);
        } else {
            animator.SetBool("IsMoving", false);
        }

        // Update the look direction based on the movement input
        if (moveInput != Vector2.zero) {
            lookDirection = moveInput.normalized;
        }

        // // Set direction for animation
        // if (moveInput.x > 0) {
        //     spriteRenderer.flipX = false; // Face right
        // } else if (moveInput.x < 0) {
        //     spriteRenderer.flipX = true; // Face left
        // }

        animator.SetFloat("LookDirectionX", lookDirection.x);
        animator.SetFloat("LookDirectionY", lookDirection.y);
    }
    
    private bool Move(Vector2 direction) {
        // Check for collisions in the direction of movement
        if (direction == Vector2.zero) {
            return false;
        }
        int count = rb.Cast(direction, contactFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0) {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }
}

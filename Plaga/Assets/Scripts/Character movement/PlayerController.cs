﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Footstep Sounds")]
    public AudioClip[] footstepClips; // Assign 3 clips here
    public float stepInterval = 0.4f; // Time between steps
    public AudioSource footstepSource;

    private float stepTimer;
    private int stepIndex = 0;

    public float moveSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    public LayerMask Interactable;
    public LayerMask solidObject;

    private Vector2 input;
    private Vector2 lastMoveDir;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (DialogueManager.DialogueIsActive)
            return;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input = input.normalized;

        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);
        animator.SetBool("isMoving", input != Vector2.zero);

        if (input != Vector2.zero)
        {
            lastMoveDir = input;

            // Step timer
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            // Reset timer when stopping
            stepTimer = 0f;
        }
    }



    private void FixedUpdate()
    {
        if (input != Vector2.zero)
        {
            Vector2 moveDelta = input * moveSpeed * Time.fixedDeltaTime;

            // Try full movement first
            if (!IsColliding(rb.position + moveDelta))
            {
                rb.MovePosition(rb.position + moveDelta);
            }
            else
            {
                // Try moving in X only
                Vector2 moveX = new Vector2(moveDelta.x, 0);
                if (!IsColliding(rb.position + moveX))
                {
                    rb.MovePosition(rb.position + moveX);
                }
                else
                {
                    // Try moving in Y only
                    Vector2 moveY = new Vector2(0, moveDelta.y);
                    if (!IsColliding(rb.position + moveY))
                    {
                        rb.MovePosition(rb.position + moveY);
                    }
                }
            }
        }
    }


    private bool IsColliding(Vector2 targetPos)
    {
        Vector2 capsuleSize = capsuleCollider.size; // Slightly smaller to avoid edge overlaps

        RaycastHit2D hit = Physics2D.BoxCast(
            targetPos,
            capsuleSize,
            0f,
            Vector2.zero,
            0f,
            solidObject
        );

        return hit.collider != null;
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0 || footstepSource == null)
            return;

        footstepSource.PlayOneShot(footstepClips[stepIndex]);

        // Cycle through the array (0 → 1 → 2 → 0 → ...)
        stepIndex = (stepIndex + 1) % footstepClips.Length;
    }


}

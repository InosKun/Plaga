using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private bool isMoving;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 input;
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Log input to debug movement
        Debug.Log("This is input.x " + input.x);
        Debug.Log("This is input.y " + input.y);

        // Set animation parameters
        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);

        // Normalize to prevent diagonal speed boost
        input = input.normalized;

        // Move player
        transform.position += (Vector3)(input * moveSpeed * Time.deltaTime);

        // Update isMoving based on whether input is being pressed
        isMoving = input != Vector2.zero;
        animator.SetBool("isMoving", isMoving);
    }

}


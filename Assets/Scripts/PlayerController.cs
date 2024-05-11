using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to change the player's movement speed
    private float moveX;
    private float moveY;
    private CharacterController controller;
    public bool canMove = true;
    private Animator animator;

    // Threshold to determine whether the player is moving
    public float movementThreshold = 0.1f;
    private bool isMoving = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Input for movement
        if (canMove)
        {
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");
        }

        // Calculate movement vector
        Vector3 movement = new Vector3(moveX, 0f, moveY) * moveSpeed * Time.deltaTime;

        // Move the player
        controller.Move(movement);

        // Rotate player to face movement direction
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

    }
}


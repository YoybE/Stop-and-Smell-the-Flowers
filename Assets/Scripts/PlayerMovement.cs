using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // Player Movement variables
    public float walkSpeed = 8;
    public float maxWalkSpeed = 8;
    public float runSpeed = 10;
    public float maxRunSpeed = 12;
    public float jumpSpeed = 5;

    private float moveHorizontal;
    private bool isJumping;
    private bool isRunning;
    private bool onGroundState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 30; // Sets application to 30 FPS
        rb = GetComponent<Rigidbody2D>(); // Gets and stores a reference to player rigidbody for access

        transform.position = new Vector3(0.0f, 0.0f, 0.0f); // Reset Player Position to Default
    }

    // Update is called once per frame
    // Game Logic Implementation
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal"); // Returns a value based on key press A/Left = -1, D/Right = 1 

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            isJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Running");
            isRunning = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { onGroundState = true; }
    }

    // FixedUpdate is called 50 times per second
    // Recommended loop for applied forces & physics (Runs on Unity Physics timestep)
    void FixedUpdate()
    {
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            float currMaxSpeed = (isRunning) ? maxRunSpeed : maxWalkSpeed;
            float currSpeed = (isRunning) ? runSpeed : walkSpeed;

            // Ensures that there is no additional acceleration past the maximum speed limit
            if (rb.linearVelocity.magnitude < currMaxSpeed)
            {
                rb.AddForce(movement * currSpeed);
            }

            Debug.Log(rb.linearVelocityX);
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            rb.linearVelocityX = 0.1f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) { isRunning = false; }

        if (isJumping && onGroundState)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse); // ForceMode.Impulse (mass dependent, time independent) 
            rb.linearVelocityX = moveHorizontal * 0.2f;

            isJumping = false;
            onGroundState = false;
        }
    }
}

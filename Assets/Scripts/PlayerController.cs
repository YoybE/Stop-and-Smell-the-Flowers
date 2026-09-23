using System;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool facingRight;
    private Vector3 originalPos;

    // Player Movement variables
    public float walkSpeed = 12.7f;
    public float maxWalkSpeed = 13.0f;
    public float runSpeed = 13.5f;
    public float maxRunSpeed = 15.0f;
    public float jumpSpeed = 11.0f;

    private float moveHorizontal;
    private bool isJumping;
    private bool isRunning;
    private bool onGroundState;

    public GameObject enemies;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 30; // Sets application to 30 FPS
        rb = GetComponent<Rigidbody2D>(); // Gets and stores a reference to player rigidbody for access
        sr = GetComponent<SpriteRenderer>(); // Gets and stores a reference to player sprite renderer for access

        originalPos = GetComponent<Transform>().position; // Retrieve original Position of Player
    }

    // Update is called once per frame
    // Game Logic Implementation
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal"); // Returns a value x, R[-1,1], based on input A/Left = -1, D/Right = 1 
        // Debug.Log(moveHorizontal);

        #region Flip Player Sprite
        if ((moveHorizontal > 0.0f) && !facingRight)
        {
            facingRight = true;
            sr.flipX = false;
        }
        else if ((moveHorizontal < 0.0f) && facingRight)
        {
            facingRight = false;
            sr.flipX = true;
        }
        #endregion

        #region Handle Movement Inputs
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            isJumping = true;
        }

        if ((moveHorizontal > 0.5) || (moveHorizontal < -0.5))
        {
            isRunning = true;
        }
        #endregion
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
            Vector2 movement = new Vector2(Mathf.Sign(moveHorizontal) * 1, 0);
            float currMaxSpeed = (isRunning) ? maxRunSpeed : maxWalkSpeed;
            float currSpeed = (isRunning) ? runSpeed : walkSpeed;

            // Ensures that there is no additional acceleration past the maximum speed limit
            if (rb.linearVelocity.magnitude < currMaxSpeed)
            {
                rb.AddForce(movement * currSpeed);
            }

            // Debug.Log(rb.linearVelocityX);
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

    public void ResetPlayer()
    {
        rb.transform.position = originalPos;
        facingRight = true;
        sr.flipX = false;
        sr.color = new Color(255, 255, 255, 255);
    }
}

using System;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using System.Collections;
using System.Security.Cryptography;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool facingRight;
    private Vector3 originalPos;

    // Player Movement variables
    public float walkSpeed = 13.0f;
    public float maxWalkSpeed = 13.5f;
    public float runSpeed = 16.0f;
    public float maxRunSpeed = 16.5f;
    public float jumpSpeed = 11.0f;
    private float currMaxSpeed;
    private float currSpeed;

    private float moveHorizontal;
    private bool isMoving = false;
    private bool isJumping;
    private bool isRunning;
    private bool onGroundState;

    // Animation
    public Animator playerAnimator;

    // Raycasting
    public float circleRadius;
    public float maxDistance;
    public LayerMask layerMask;

    // SFX
    public AudioClip hitSFX;
    public AudioMixerGroup audioMixerGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 30; // Sets application to 30 FPS

        rb = GetComponent<Rigidbody2D>(); // Gets and stores a reference to player rigidbody for access
        sr = GetComponent<SpriteRenderer>(); // Gets and stores a reference to player sprite renderer for access

        originalPos = GetComponent<Transform>().position; // Retrieve original Position of Player
        facingRight = true;
    }

    // Update is called once per frame
    // Game Logic Implementation
    void Update()
    {
        if (Time.timeScale != 0.0f)
        {
            playerAnimator.SetBool("onGround", onGroundState);
            playerAnimator.SetBool("isIdle", Mathf.Abs(rb.linearVelocityX) < 0.0001f);
            playerAnimator.SetFloat("xSpeed", Mathf.Abs(rb.linearVelocityX));
            playerAnimator.SetBool("isRunning", isRunning);
            // Debug.Log($"xVelocity: {rb.linearVelocityX}\nisRunning: {isRunning}");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { onGroundState = true; }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(rb, sr.color, 1);
        }
        if (collision.gameObject.CompareTag("Breakable"))
        {
            if (SASTFUtils.Utils.compareColor(sr.color, collision.gameObject.GetComponent<SpriteRenderer>().color))
            {
                collision.transform.parent.gameObject.SetActive(false);
                AudioManager.instance.PlaySFX(hitSFX, 0.3f, 1.0f, audioMixerGroup);
                GameManager.instance.score++;
            }
        }
    }

    // FixedUpdate is called 50 times per second
    // Recommended loop for applied forces & physics (Runs on Unity Physics timestep)
    void FixedUpdate()
    {
        if (isMoving)
        {
            Move(facingRight == true ? 1 : -1);
        }
    }

    public void ResetPlayer()
    {
        rb.transform.position = originalPos;
        facingRight = true;
        sr.flipX = false;
        sr.color = new Color(255, 255, 255, 255);
        isRunning = false;
        isMoving = false;
        rb.linearVelocityX = 0;
    }

    #region Player Actions
    void FlipSprite(int value)
    {
        if (Time.timeScale != 0.0f)
        {
            // If the player has pressed left button, flip the sprite to the left; vice versa
            if (value == -1 && facingRight)
            {
                facingRight = false;
                sr.flipX = true;
            }
            else if (value == 1 && !facingRight)
            {
                facingRight = true;
                sr.flipX = false;
            }
        }
    }

    void Move(int moveHorizontal)
    {
        // Debug.Log("Player is moving...");
        Vector2 movement = new Vector2(Mathf.Sign(moveHorizontal) * 1, 0);
        currMaxSpeed = (isRunning) ? maxRunSpeed : maxWalkSpeed;
        currSpeed = (isRunning) ? runSpeed : walkSpeed;

        // Ensures that there is no additional forces past the maximum speed limit
        if (Mathf.Abs(rb.linearVelocityX) < currMaxSpeed)
        {
            float limiter = (currMaxSpeed - Mathf.Abs(rb.linearVelocityX)) / currMaxSpeed;
            rb.AddForce(movement * currSpeed * limiter);
        }

        // Debug.Log(rb.linearVelocityX);
    }

    public void MoveCheck(int value)
    {
        if (Time.timeScale != 0.0f)
        {
            if (value == 0) { isMoving = false; }
            else
            {
                Debug.Log($"Move Value: {value}");
                FlipSprite(value);
                isMoving = true;
                Move(value);
            }
        }
    }

    public void Sprint(int value)
    {
        // Debug.Log("Sprint called");
        if (value == 0) { isRunning = false; }
        else if (value == 1) { isRunning = true; }
    }

    public void Jump()
    {
        // Debug.Log("Jump called");
        if (Time.timeScale != 0.0f)
        {
            isJumping = true;
            if (isJumping && onGroundState)
            {
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse); // ForceMode.Impulse (mass dependent, time independent) 
                rb.linearVelocityX = moveHorizontal * 0.2f;

                isJumping = false;
                onGroundState = false;
            }
        }
    }
    #endregion
    // void triggerBlast()
    // {
    //     RaycastHit2D circleHit = Physics2D.CircleCast(transform.position, circleRadius, -transform.up, maxDistance, layerMask);
    //     if (circleHit)
    //     {
    //         Debug.Log($"{circleHit.collider.name} has been hit");
    //         float originalX = circleHit.collider.gameObject.transform.position.x;

    //         circleHit.collider.transform.parent.gameObject.SetActive(false);
    //         AudioManager.instance.PlaySFX(hitSFX, 0.3f, 1.0f);
    //     }
    // }

    // // Helper to visualize raycast
    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawSphere(transform.position - transform.up * maxDistance, circleRadius);
    // }
}

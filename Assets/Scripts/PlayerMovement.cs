using System;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool facingRight;
    private Vector3 originalPos;

    // Player Movement variables
    public float walkSpeed = 8.1f;
    public float maxWalkSpeed = 8.2f;
    public float runSpeed = 9.0f;
    public float maxRunSpeed = 12;
    public float jumpSpeed = 10;

    private float moveHorizontal;
    private bool isJumping;
    private bool isRunning;
    private bool onGroundState;

    [SerializeField] private int score;
    public GameObject enemies;

    // UI
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public GameObject otherUI;
    // private Vector3 restartOriginalPos = new Vector3(524.5f, 281.0f, 0.0f);
    // public Vector3 restartFinalPos = new Vector3(0.0f, -20.0f, 0.0f);

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
        moveHorizontal = Input.GetAxisRaw("Horizontal"); // Returns a value based on key press A/Left = -1, D/Right = 1 

        #region Flip Player Sprite
        if ((moveHorizontal > 0) && !facingRight)
        {
            facingRight = true;
            sr.flipX = false;
        }
        else if ((moveHorizontal < 0) && facingRight)
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Running");
            isRunning = true;
        }
        #endregion
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { onGroundState = true; }
        if (collision.gameObject.CompareTag("Enemy")) { GameOver(); }
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

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        otherUI.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart");
        ResetGame();
        Time.timeScale = 1.0f;
    }

    public void ResetGame()
    {
        rb.transform.position = originalPos;
        facingRight = true;
        sr.flipX = false;
        scoreText.text = "Score: 0";
        otherUI.SetActive(true);
        gameOverScreen.SetActive(false);
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyBehaviour>().startPosition;
            eachChild.GetComponent<EnemyBehaviour>().hp = 1;
            eachChild.gameObject.SetActive(true);
        }
    }
}

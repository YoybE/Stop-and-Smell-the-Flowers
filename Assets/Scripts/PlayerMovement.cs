using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // Player Movement variables
    public float speed = 10;
    public float maxSpeed = 20;

    private float moveHorizontal;
    private bool isJumping;

    // KeyMapping
    public KeyCode[] Jump = { KeyCode.Space, KeyCode.Joystick1Button0 };

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

        for (int i = 0; i < Jump.Length; i++)
        {
            // TODO: Fix Issue where bool does not update with first array element
            isJumping = Input.GetKeyDown(Jump[i]);
        }
    }

    // FixedUpdate is called 50 times per second
    // Recommended loop for applied forces & physics
    void FixedUpdate()
    {
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);

            // Ensures that there is no additional acceleration past the maximum speed limit
            if (rb.linearVelocity.magnitude < maxSpeed)
            {
                rb.AddForce(movement * speed); // Kept as ForceMode.Force (mass & time dependent) 
                Debug.Log(moveHorizontal);
            }
        }

        if (isJumping)
        {
            Debug.Log("Is Jumping");
            Vector2 movement = new Vector2(0, 1);
            rb.AddForce(movement * speed, ForceMode2D.Impulse); // ForceMode.Impulse (mass dependent, time independent) 
        }
    }
}

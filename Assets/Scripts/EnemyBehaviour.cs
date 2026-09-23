using System;
using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    [NonSerialized] public Vector3 startPosition;

    // Movement/Patrol Variables
    public float patrolSpeed = 2;
    private float originalX;
    public float maxOffset = 1.0f;
    public float patrolTime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    // Enemy Attributes
    private Color enemyColor;
    public int hp = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyColor = GetComponent<SpriteRenderer>().color;
        startPosition = GetComponent<Transform>().position;

        originalX = transform.position.x;
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / patrolTime, 0);
    }

    void move()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        // if (hp == 0) { gameObject.SetActive(false); }
    }

    // FixedUpdate is called 50 times per second
    // Recommended loop for applied forces & physics (Runs on Unity Physics timestep)
    void FixedUpdate()
    {
        if (Mathf.Abs(rb.position.x - originalX) < maxOffset) { move(); }
        else
        {
            moveRight *= -1;
            ComputeVelocity();
            move();
        }
    }

    public void takeDamage(int damage)
    {
        hp -= damage;
    }
}

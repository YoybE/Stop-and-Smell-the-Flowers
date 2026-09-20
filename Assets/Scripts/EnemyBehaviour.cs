using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    // Movement/Patrol Variables
    public float patrolSpeed = 2;
    private float originalX;
    public float maxOffset = 1.0f;
    public float patrolTime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    // Enemy Attributes
    private Color enemyColor;
    [SerializeField] private int hp = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyColor = GetComponent<SpriteRenderer>().color;

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

    }

    // FixedUpdate is called 50 times per second
    // Recommended loop for applied forces & physics
    void FixedUpdate()
    {
        if (Mathf.Abs(rb.position.x - originalX) < maxOffset) { move(); }
        else
        {
            Debug.Log(enemyColor);
            moveRight *= -1;
            ComputeVelocity();
            move();
        }
    }
}

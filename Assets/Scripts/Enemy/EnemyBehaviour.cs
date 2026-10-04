using System;
using TMPro;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    [NonSerialized] public Vector3 startPosition;
    public Collider2D enemyCollider;

    // Movement/Patrol Variables
    public float patrolSpeed = 2;
    private float originalX;
    public float maxOffset = 1.0f;
    public float patrolTime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    // Enemy Attributes
    public Color enemyColor;
    public int initialHp = 2;
    public int hp;

    // Audio SFX
    public AudioClip hitSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyColor = GetComponent<SpriteRenderer>().color;
        startPosition = GetComponent<Transform>().position;

        originalX = transform.position.x;
        hp = initialHp;
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
        if (hp == 0)
        {
            GameManager.instance.score++;
            gameObject.SetActive(false);
        }
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

    public void TakeDamage(Rigidbody2D playerRb, Color color, int damage)
    {
        if (compareColor(enemyColor, color))
        {
            Vector2 recoil = -2.5f * playerRb.linearVelocity;

            Debug.Log("Killing Enemy");
            hp -= damage;
            AudioManager.instance.PlaySFX(hitSFX, 0.3f, 1.0f);
            Invulnerability(GetComponent<SpriteRenderer>(), enemyCollider);
            playerRb.AddForce(recoil, ForceMode2D.Impulse);
        }
        else
        {
            GameManager.instance.GameOver();
            Debug.Log($"Unable to kill enemy character is not of same color ({color} != {enemyColor})");
        }
    }

    bool compareColor(Color color1, Color color2)
    {
        bool compareColorComponent(float c1, float c2)
        {
            return Mathf.Abs(c1 - c2) < 0.1f;
        }

        return compareColorComponent(color1.r, color2.r) & compareColorComponent(color1.g, color2.g) & compareColorComponent(color1.b, color2.b) & compareColorComponent(color1.a, color2.a);
    }

    void Invulnerability(SpriteRenderer sr, Collider2D col)
    {
        StartCoroutine(playAnimation());

        IEnumerator playAnimation()
        {
            col.enabled = false;

            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                {
                    sr.color = Color.white;
                }
                else
                {
                    sr.color = Color.black;
                }
                yield return new WaitForSeconds(0.2f);
            }

            col.enabled = true;
            sr.color = enemyColor;
        }
    }
}

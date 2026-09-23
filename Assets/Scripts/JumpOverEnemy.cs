using TMPro;
using UnityEngine;

public class JumpOverEnemy : MonoBehaviour
{
    /*
    Implements a raycast below the player to detect 
    */
    private bool onGroundState;

    private bool countScoreState = false;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    private RaycastHit2D box;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called 50 times per second
    // Recommended loop for applied forces & physics (Runs on Unity Physics timestep)
    void FixedUpdate()
    {
        // Player Jumps
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            // onGroundState = false;
            // countScoreState = true;
        }

        // if (!onGroundState && countScoreState && onEnemyCheck())
        if (onEnemyCheck())
        {
            damageEnemy();
            // GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            // countScoreState = false;
        }
    }

    private void damageEnemy()
    {
        // Debug.Log("Collided with:" + box.collider.name);
        // if (box.collider.name.Equals("Enemy"))
        // {
        Debug.Log("on enemy");
        if (transform.GetComponent<SpriteRenderer>().color == box.collider.gameObject.GetComponent<SpriteRenderer>().color)
        {
            Debug.Log("Killing Enemy");
            // box.collider.gameObject.GetComponent<EnemyBehaviour>().takeDamage(1);
            box.collider.gameObject.SetActive(false);
            GameManager.instance.score++;
        }
        else
        {
            GameManager.instance.GameOver();
            Debug.Log("Unable to kill enemy character is not of same color");
        }
        // }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { onGroundState = true; }
        if (collision.gameObject.CompareTag("Enemy")) { GameManager.instance.GameOver(); }
    }

    // Helper to visualize boxSize
    void OnDrawGizmos()
    {
        Gizmos.color = Color.pink;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    private bool onEnemyCheck()
    {
        box = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask);
        if (box)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

using TMPro;
using UnityEngine;

public class JumpOverEnemy : MonoBehaviour
{
    /*
    Implements a raycast below the player to detect 
    */
    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    private bool onGroundState;

    [System.NonSerialized]
    public int score = 0;

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
            onGroundState = false;
            countScoreState = true;
        }

        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                Debug.Log(score);
                damageEnemy();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { onGroundState = true; }
    }

    private void damageEnemy()
    {
        box = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask);
        if (box.collider != null)
        {
            if (box.collider.CompareTag("Enemy"))
            {
                Debug.Log("on enemy");
                box.collider.gameObject.GetComponent<EnemyBehaviour>().takeDamage(1);
                score++;
                scoreText.text = "Score: " + score.ToString();
            }
        }
        else
        {
            Debug.Log("not on enemy");
        }
    }

    // Helper to visualize boxSize
    void OnDrawGizmos()
    {
        Gizmos.color = Color.pink;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
}

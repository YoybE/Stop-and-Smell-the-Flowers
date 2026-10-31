using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// GameManager handles global player data (such as score)
public class GameManager : MonoBehaviour
{
    [Header("Game Constants")]
    public GameConstants gameConstants;
    public IntVariable gameScore; // Stores the player score
    public ListIntVariable colorScore; // List of binary integers that indicate whether a color has been found, based off the colors of the Rainbow

    [Header("Runtime Sets")]
    [SerializeField] private SimpleRuntimeSet _enemyRuntimeSet;
    [SerializeField] private SimpleRuntimeSet _interactableRuntimeSet;

    // public GameObject player;
    // public GameObject enemies;
    // public GameObject interactables;
    public GameObject breakables;
    // public GameObject camera;

    void Awake()
    {
        gameScore.Initialize();
        colorScore.SetValue(new List<int> { 0, 0, 0, 0, 0, 0, 0 });
        colorScore.Initialize();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // colorScore.SetValue(new List<int> { 0, 0, 0, 0, 0, 0, 0 });
        // AudioManager.instance.PlaySFX(gameConstants.backgroundMusic, 1.0f, 1.0f, gameConstants.MusicMixerGroup, true);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
    }

    public void ResetGame()
    {
        if (SceneManager.GetActiveScene().name != "Forest")
        {
            SceneManager.LoadScene("Forest");
            Time.timeScale = 1.0f;
        }

        gameScore.Initialize();
        colorScore.Initialize();

        // enemies = GameObject.FindGameObjectWithTag("Enemies");
        // interactables = GameObject.FindGameObjectWithTag("Interactable");
        breakables = GameObject.FindGameObjectWithTag("Breakable");

        foreach (GameObject enemy in _enemyRuntimeSet.allItems)
        {
            enemy.transform.localPosition = enemy.GetComponent<EnemyBehaviour>().startPosition;
            enemy.GetComponent<EnemyBehaviour>().hp = enemy.GetComponent<EnemyBehaviour>().initialHp;
            enemy.GetComponent<SpriteRenderer>().color = enemy.GetComponent<EnemyBehaviour>().enemyColor;
            enemy.GetComponent<Collider2D>().enabled = true;
            enemy.SetActive(true);
        }

        foreach (GameObject interactable in _interactableRuntimeSet.allItems)
        {
            interactable.GetComponent<Interactable>().ResetAlpha();
            interactable.gameObject.SetActive(true);
        }

        foreach (Transform eachChild in breakables.transform)
        {
            eachChild.gameObject.SetActive(true);
        }

        Time.timeScale = 1.0f;
    }
}

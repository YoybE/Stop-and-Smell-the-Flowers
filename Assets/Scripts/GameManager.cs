using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

// GameManager handles global player data (such as score)
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public int score; // Stores the player score
    public List<int> colorScore; // List of binary integers that indicate whether a color has been found, based off the colors of the Rainbow
    public GameObject player;
    public GameObject enemies;
    public GameObject interactables;
    public GameObject camera;

    private void Awake()
    {
        // Ensures that there is only one instance of this Singleton Class
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initializes the scores
        score = 0;
        colorScore = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
    }

    public void UpdateColorScore(int index)
    {
        colorScore[index] = 1;
        UIManager.instance.UpdateColorScoreUI(index);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        UIManager.instance.GameOver(true);
    }

    public void ResetGame()
    {
        player.GetComponent<PlayerController>().ResetPlayer();
        UIManager.instance.GameOver(false);
        UIManager.instance.ResetUI();
        score = 0;
        colorScore = new List<int> { 0, 0, 0, 0, 0, 0, 0 };

        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyBehaviour>().startPosition;
            eachChild.GetComponent<EnemyBehaviour>().hp = 1;
            eachChild.gameObject.SetActive(true);
        }

        foreach (Transform eachChild in interactables.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<Interactable>().startPosition;
            eachChild.GetComponent<Interactable>().ResetAlpha();
            eachChild.gameObject.SetActive(true);
        }
        camera.transform.position = new Vector3(4.19f, 3.01f, -10);
        Time.timeScale = 1.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// GameManager handles global player data (such as score)
public class GameManager : Singleton<GameManager>
{
    public GameConstants gameConstants;
    public IntVariable gameScore; // Stores the player score
    public List<int> colorScore; // List of binary integers that indicate whether a color has been found, based off the colors of the Rainbow
    public GameObject player;
    public GameObject enemies;
    public GameObject interactables;
    public GameObject breakables;
    public GameObject camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initializes the scores
        gameScore.SetValue(0);
        colorScore = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
        AudioManager.instance.PlaySFX(gameConstants.backgroundMusic, 1.0f, 1.0f, gameConstants.MusicMixerGroup, true);
    }

    public void UpdateColorScore(int index)
    {
        colorScore[index] = 1;
        UIManager.instance.UpdateColorScoreUI(index);
    }

    public void GameOver()
    {
        StopAllAudio();

        Time.timeScale = 0.0f;
        UIManager.instance.GameOver(true);
    }

    public void ResetGame()
    {
        if (SceneManager.GetActiveScene().name != "Forest")
        {
            SceneManager.LoadScene("Forest");
            Time.timeScale = 1.0f;
            AudioManager.instance.PlaySFX(gameConstants.backgroundMusic, 1.0f, 1.0f, gameConstants.MusicMixerGroup, true);
        }

        player.GetComponent<PlayerController>().ResetPlayer();
        UIManager.instance.GameOver(false);
        UIManager.instance.ResetUI();
        gameScore.ResetHighestValue();
        gameScore.SetValue(0);
        colorScore = new List<int> { 0, 0, 0, 0, 0, 0, 0 };

        enemies = GameObject.FindGameObjectWithTag("Enemies");
        interactables = GameObject.FindGameObjectWithTag("Interactable");
        breakables = GameObject.FindGameObjectWithTag("Breakable");

        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyBehaviour>().startPosition;
            eachChild.GetComponent<EnemyBehaviour>().hp = eachChild.GetComponent<EnemyBehaviour>().initialHp;
            eachChild.gameObject.GetComponent<SpriteRenderer>().color = eachChild.GetComponent<EnemyBehaviour>().enemyColor;
            eachChild.gameObject.GetComponent<Collider2D>().enabled = true;
            eachChild.gameObject.SetActive(true);
        }

        foreach (Transform eachChild in interactables.transform)
        {
            eachChild.GetComponent<Interactable>().ResetAlpha();
            eachChild.gameObject.SetActive(true);
        }

        foreach (Transform eachChild in breakables.transform)
        {
            eachChild.gameObject.SetActive(true);
        }

        camera.transform.position = new Vector3(4.19f, 3.01f, -10);
        Time.timeScale = 1.0f;

        StopAllAudio();
        AudioManager.instance.PlaySFX(gameConstants.backgroundMusic, 1.0f, 1.0f, gameConstants.MusicMixerGroup, true);
    }

    public void StopAllAudio()
    {
        AudioSource[] audioSources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in audioSources)
        {
            audio.Stop();
        }
    }
}

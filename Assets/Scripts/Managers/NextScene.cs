using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextSceneName;
    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is exiting area...");
            ChangeScene();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    int currentSceneIndex;

    PlayerMovement playerMovement;
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(LoadStartScene());
        }
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    IEnumerator LoadStartScene()
    {
        yield return new WaitForSeconds(5);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadInstructionsScene()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void TryAgainButton()
    {
        playerMovement.scoreCanvas.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }
}

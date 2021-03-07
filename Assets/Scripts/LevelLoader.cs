using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    int currentSceneIndex;
    //[SerializeField] GameObject menuCanvas;
    PlayerMovement playerMovement;
    
    string currentScene;
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(LoadStartScene());
        }
        playerMovement = FindObjectOfType<PlayerMovement>();
        //playfab = FindObjectOfType<PlayFabLogin1>();
    }

    // void Update()
    //{
    //    currentScene = SceneManager.GetActiveScene().name;
     //   if(currentScene == "GameScene")
     //   {
      //      menuCanvas.SetActive(false);
       // }
  //  }

    IEnumerator LoadStartScene()
    {
        yield return new WaitForSeconds(5);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        //playfab.loginCanvas.SetActive(true);
        SceneManager.LoadScene(currentSceneIndex + 1);
        //menuCanvas.SetActive(true);
    }

    public void LoadGameScene()
    {
        //menuCanvas.SetActive(false);
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
        //playerMovement.scoreCanvas.SetActive(false);
        //playfab.loginCanvas.SetActive(false);
        SceneManager.LoadScene("GameScene");
        //menuCanvas.SetActive(false);
    }

    public void MainMenuButton()
    {
        
        SceneManager.LoadScene("MainMenu");
        //menuCanvas.SetActive(true);
    }

    public void LeaderBoardButton()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}

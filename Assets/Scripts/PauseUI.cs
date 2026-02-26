using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
// using System.Collections;
// using System.Collections.Generic;

public class PauseUI : MonoBehaviour
{
   public GameObject pauseMenu;
    public static bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    //void Update() {}
    
    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if(isPaused)
        {
            ResumeGame();
        } else
        {
            PauseGame();
        }

    }


    private void PauseGame()
    {
        // Debug.Log("Paused Game");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void ResumeGame()
    {
        // Debug.Log("Unpaused Game");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Exiting to Main Menu");
        // Update included scenes in build profile
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Application");
        // Time.timeScale = 1f;
        // Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    void Update() {}
    
    // public void OnPauseInput(InputAction.CallbackContext context)
    // {
    //     if(isPaused)
    //     {
    //         ResumeGame();
    //     } else
    //     {
    //         PauseGame();
    //     }
    // }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // public void ReturnToMainMenu()
    // {
    //     //SceneManager.LoadSceneAsync("MainMenu");
    // }

}

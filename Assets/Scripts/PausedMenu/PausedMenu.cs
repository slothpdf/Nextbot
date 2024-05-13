using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public GameObject pausedMenu;
    public GameObject settings; 
    public PlayerControls playerControls;

    public bool paused = false;

    void Start()
    {
       
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused == false)
            {
                pausedMenu.SetActive(true);
                paused = true;

                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (playerControls != null)
                {
                    playerControls.enabled = false;
                }
            }

            else if (paused == true)
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        pausedMenu.SetActive(false);
        paused = false;

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if(playerControls != null)
        {
            playerControls.enabled = true;
        }

 
    }

    public void GoToMenu(string MainMenu)
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartGameButton(string world)
    {
        SceneManager.LoadScene("world");
        ResumeGame();
    }

    public void OpenSettings()
    {
        if (settings != null)
        {
            settings.SetActive(true);
        }
    }
}

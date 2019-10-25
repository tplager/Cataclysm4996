using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                ResumeGame(); 
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming");
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        paused = false;
    }


    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;
    //List<GameObject> blocks;

    void Start()
    {
        ResumeGame();
        //List<GameObject> blocks = Camera.main.GetComponent<GameController>().BlockObjects;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                //GameObject.Find("Resume").onClick.Invoke();
                ResumeGame();
            }

            if (!paused)
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming");
        pauseMenu.SetActive(false);
        List<GameObject> blocks = Camera.main.GetComponent<GameController>().BlockObjects;
        for (int i = 0; i < blocks.Count - 1; i++)
        {
            blocks[i].GetComponent<BlockMovement>().enabled = true;
        }
        paused = false;
    }

    void PauseGame()
    {
        Debug.Log("Pausing");
        pauseMenu.SetActive(true);
        List<GameObject> blocks = Camera.main.GetComponent<GameController>().BlockObjects;
        for (int i = 0; i < blocks.Count - 1; i++)
        {
            blocks[i].GetComponent<BlockMovement>().enabled = false;
        }
        paused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;
    public bool ableToPause = true; 

    void Start()
    {
        ResumeGame();
        //List<GameObject> blocks = Camera.main.GetComponent<GameController>().BlockObjects;
    }

    // Update is called once per frame
    void Update()
    {
        if (ableToPause)
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
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming");
        pauseMenu.SetActive(false);
        //List<GameObject> blocks = Camera.main.GetComponent<GameController>().BlockObjects;
        //for (int i = 0; i < blocks.Count - 1; i++)
        //{
        //    blocks[i].GetComponent<BlockMovement>().enabled = true;
        //}

        Camera.main.GetComponent<GameController>().TurnOnMovement();

        paused = false;

        GameObject.Find("MovesRemain").GetComponent<MovesRemaining>().CountingDown = true;
    }

    public void PauseGame()
    {
        Debug.Log("Pausing");
        pauseMenu.SetActive(true);
        //List<GameObject> blocks = Camera.main.GetComponent<GameController>().BlockObjects;
        //for (int i = 0; i < blocks.Count - 1; i++)
        //{
        //    blocks[i].GetComponent<BlockMovement>().enabled = false;
        //}

        Camera.main.GetComponent<GameController>().TurnOffMovement(); 

        paused = true;

        GameObject.Find("MovesRemain").GetComponent<MovesRemaining>().CountingDown = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

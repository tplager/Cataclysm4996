using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the pause menu in the game
/// 
/// author: Darius James daj7160@rit.edu
/// </summary>
public class PauseMenu : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool paused = false;        //is the game currently paused?
    [SerializeField]
    private GameObject pauseMenu;       //the gameObject containing the pauseMenu's canvas
    [SerializeField]
    private bool ableToPause = true;    //is the game able to be paused right now?
    #endregion

    #region Properties
    public bool Paused { get { return paused; } }
    public bool AbleToPause
    {
        get { return ableToPause; }
        set { ableToPause = value; }
    }
    #endregion
    void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        //if the game can be paused check if escape has been hit
        //if it has, call either pause or resume game depending
        //on if the game is currently paused or not
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

    /// <summary>
    /// Resume the game
    /// </summary>
    public void ResumeGame()
    {
        //turning off the pause canvas
        //Debug.Log("Resuming");
        pauseMenu.SetActive(false);

        //turning on all block movement
        Camera.main.GetComponent<GameController>().TurnOnMovement();

        //setting paused = false
        paused = false;

        //setting counting down to true so moves will count down again
        GameObject.Find("MovesRemain").GetComponent<MovesRemaining>().CountingDown = true;
    }

    /// <summary>
    /// Pause the game
    /// </summary>
    public void PauseGame()
    {
        //turn on the pause canvas
        //Debug.Log("Pausing");
        pauseMenu.SetActive(true);

        //turn off all block movement
        Camera.main.GetComponent<GameController>().TurnOffMovement(); 

        //set paused = true
        paused = true;

        //set counting down to false so moves won't be counted while paused
        GameObject.Find("MovesRemain").GetComponent<MovesRemaining>().CountingDown = false;
    }

    /// <summary>
    /// Quits the Game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}

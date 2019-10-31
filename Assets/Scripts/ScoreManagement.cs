using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Holds global variables that multiple scripts need access to
/// Calls path check on all of the blocks
/// 
/// authors: Trenton Plager tlp6760@rit.edu, Ethan Harris evh5516@rit.edu
/// </summary>
public class ScoreManagement : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private int totalScore;     //the total score for the player; resets when they go back to the main menu
    private bool restarted;     //determines whether the game has been restarted or not
    private int levelScore;     //the score for the player on the current level
    private bool finishedGame;  //whether the game has been finished or not
    #endregion

    #region Properties 
    public bool Restarted
    {
        get { return restarted; }
        set { restarted = value; }
    }
    public int LevelScore
    {
        get { return levelScore; }
        set { levelScore = value; }
    }
    public int TotalScore
    {
        get { return totalScore; }
        set { totalScore = value; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        restarted = false;
        finishedGame = false;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //If the scene name is Credits1 and the game hasn't been finished
        //Find the TotalScore object and update its text
        //also change the finished game value to true
        if (SceneManager.GetActiveScene().name == "Credits1" && !finishedGame)
        {
            GameObject.Find("TotalScore").GetComponent<Text>().text += totalScore;
            finishedGame = true;
        }
    }

    /// <summary>
    /// End the level and update the total score
    /// </summary>
    public void EndLevel()
    {
        totalScore += levelScore;
    }
}

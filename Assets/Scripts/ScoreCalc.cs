using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Calculates the current score
/// 
/// author: Darius James daj7160@rit.edu
/// </summary>
public class ScoreCalc : MonoBehaviour
{
    #region Fields
    private int score;                      //the current score for the player
    [SerializeField]
    private Text scoreText;                 //the text object to display the score
    [SerializeField]
    private GameObject moveCounter;         //the gameObject containing the MovesRemaining object
    [SerializeField]
    private MovesRemaining moves;           //the level's MovesRemaining moves
    private ScoreManagement scoreManager;   //the current ScoreManagment Object
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //setting initial variables
        moveCounter = GameObject.Find("MovesRemain");
        moves = moveCounter.GetComponent<MovesRemaining>();

        //try to retrieve the ScoreManager object if it exists
        //otherwise try to retrieve the ScoreManager(Clone) object
        try
        {
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManagement>();
        }
        catch (Exception e)
        {
            scoreManager = GameObject.Find("ScoreManager(Clone)").GetComponent<ScoreManagement>(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        //update the score
        score = (moves.StartingMoves + moves.MovesRemain) * 100;
        scoreManager.LevelScore = score;

        //if the score goes below 0 set it to 0
        if (score < 0) score = 0;

        //update score text
        scoreText.text = "Score: " + score;
    }
}

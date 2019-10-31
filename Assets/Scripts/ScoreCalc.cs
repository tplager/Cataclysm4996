using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalc : MonoBehaviour
{
    private int score;
    public Text scoreText;
    GameObject moveCounter;
    MovesRemaining moves;
    public ScoreManagement scoreManager; 

    // Start is called before the first frame update
    void Start()
    {
        moveCounter = GameObject.Find("MovesRemain");
        moves = moveCounter.GetComponent<MovesRemaining>();

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
        score = (moves.StartingMoves + moves.MovesRemain) * 100;
        scoreManager.LevelScore = score;

        if (score < 0)
        {
            score = 0;
        }

        scoreText.text = "Score: " + score;
    }
}

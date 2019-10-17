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
    // Start is called before the first frame update
    void Start()
    {
        moveCounter = GameObject.Find("MovesRemain");
        moves = moveCounter.GetComponent<MovesRemaining>();
    }

    // Update is called once per frame
    void Update()
    {
        score = (moves.startingMoves + moves.movesRemaining) * 100;

        if (score < 0)
        {
            score = 0;
        }

        scoreText.text = "Score: " + score;
    }
}

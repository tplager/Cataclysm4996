using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Determines how many moves the player has remaining
/// 
/// author: Darius James daj7160@rit.edu
/// </summary>
public class MovesRemaining : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private int startingMoves;      //the player's starting moves
    [SerializeField]
    private int movesRemaining;     //the player's remaining moves
    [SerializeField]
    private Text movesText;         //the text object to display the player's remaining moves
    private bool countingDown;      //should the moves be counted down right now?
    #endregion

    #region Properties
    public bool CountingDown
    {
        get { return countingDown;  }
        set { countingDown = value; }
    }
    public int MovesRemain { get { return movesRemaining; } }
    public int StartingMoves { get { return startingMoves; } }
    #endregion

    void Start()
    {
        //initializing starting values
        movesRemaining = startingMoves;
        countingDown = true;
    }
    void Update()
    {
        //update the moves remaining text
        movesText.text = "Moves Remaining: " + movesRemaining;
    }

    /// <summary>
    /// Remove a move from movesRemaining if counting down
    /// If it hits 0 display the lose canvas and pause the game
    /// </summary>
    public void RemoveMoves()
    {
        if (countingDown) movesRemaining--;

        // If it hits 0 display the lose canvas and pause the game
        if (movesRemaining <= 0)
        {
            GameObject.Find("LoseCanvas").GetComponent<Canvas>().enabled = true;

            Camera.main.GetComponent<GameController>().TurnOffMovement();

            GameObject.Find("PauseCanvas").GetComponent<PauseMenu>().AbleToPause = false;

            GameObject.Find("MovesRemain").GetComponent<MovesRemaining>().CountingDown = false;
        }
    }
}

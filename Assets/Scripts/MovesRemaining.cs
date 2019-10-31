using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovesRemaining : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private int startingMoves;
    [SerializeField]
    private int movesRemaining;
    [SerializeField]
    private Text movesText;
    private bool countingDown;
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
        movesRemaining = startingMoves;
        countingDown = true;
    }
    void Update()
    {
        movesText.text = "Moves Remaining: " + movesRemaining;

    }

    public void RemoveMoves()
    {
        if (countingDown) movesRemaining--;

        if (movesRemaining < 0)
        {
            GameObject.Find("LoseCanvas").GetComponent<Canvas>().enabled = true;

            Camera.main.GetComponent<GameController>().TurnOffMovement();

            GameObject.Find("PauseCanvas").GetComponent<PauseMenu>().ableToPause = false;

            GameObject.Find("MovesRemain").GetComponent<MovesRemaining>().CountingDown = false;
        }
    }
}

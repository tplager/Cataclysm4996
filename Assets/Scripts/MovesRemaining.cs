using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovesRemaining : MonoBehaviour
{
    
    public int startingMoves;
    public int movesRemaining;
    public Text movesText;
    
    void Start()
    {
         movesRemaining = startingMoves;
    }
    void Update()
    {
        movesText.text = "Moves Remaining: " + movesRemaining;
        if(Input.GetMouseButtonDown(0))
        {
            movesRemaining--;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovesRemaining : MonoBehaviour
{
    public int movesRemaining;
    public Text movesText;

    void Update()
    {
        movesText.text = "Moves Remaining: " + movesRemaining;
        if(Input.GetMouseButtonDown(0))
        {
            movesRemaining--;
        }
    }
}

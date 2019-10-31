using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagement : MonoBehaviour
{
    #region Fields
    private int totalScore;
    private bool restarted;
    private int levelScore;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndLevel()
    {
        totalScore += levelScore;
    }
}

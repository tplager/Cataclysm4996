using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class ScoreManagement : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private int totalScore;
    private bool restarted;
    private int levelScore;
    private bool finishedGame;
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
        if (SceneManager.GetActiveScene().name == "Credits1" && !finishedGame)
        {
            GameObject.Find("TotalScore").GetComponent<Text>().text += totalScore;
            finishedGame = true;
        }
    }

    public void EndLevel()
    {
        totalScore += levelScore;
    }
}

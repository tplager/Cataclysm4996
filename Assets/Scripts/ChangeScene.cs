using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System; 

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeTo(string name)
    {
        if (name == "MainMenu")
        {
            try
            {
                GameObject.Find("ScoreManager").GetComponent<ScoreManagement>().TotalScore = 0;
            }
            catch (Exception e)
            {
                GameObject.Find("ScoreManager(Clone)").GetComponent<ScoreManagement>().TotalScore = 0;
            }
        }
        SceneManager.LoadScene(name);
    }
}

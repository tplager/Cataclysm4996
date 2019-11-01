using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Changes the current scene
/// 
/// author: Darius James daj7160@rit.edu
/// </summary>
public class ChangeScene : MonoBehaviour
{
    /// <summary>
    /// Change the scene to a scene with the passed-in name
    /// </summary>
    /// <param name="name">The name of the scene to change to</param>
    public void ChangeTo(string name)
    {
        //if we're changing to main menu from one of other levels
        //reset the total score
        if (name == "MainMenu" && SceneManager.GetActiveScene().name != "HowToPlay" && SceneManager.GetActiveScene().name != "LevelSelect")
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

        try
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayButtonClick(); 
        }
        catch (Exception e)
        {
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().PlayButtonClick();
        }

        //load the passed-in scene
        SceneManager.LoadScene(name);
    }
}

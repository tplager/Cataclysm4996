using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Holds global variables that multiple scripts need access to
/// Calls path check on all of the blocks
/// 
/// author: Trenton Plager tlp6760@rit.edu
/// </summary>
public class GameController : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private List<GameObject> blockObjects;                  //a list of all blocks in the scene
    private List<PathCheck> pathCheckScripts;               //a list of all of the path check scripts in the scene
    [SerializeField]   
    private int pathRaycastDistance;                        //how far the path check will raycast
    [SerializeField]
    private GameObject exitPiece;                           //the exit piece in the scene
    [SerializeField]
    private GameObject scoreManagerPrefab;                  //the score manager prefab so it can be created if there isn't one
    private Dictionary<string, bool> occupiedPositions;     //a dictionary of strings mapped to booleans represeting whether a spot is occupied or not
    #endregion

    #region Properties
    public List<GameObject> BlockObjects { get { return blockObjects; } }
    public Dictionary<string, bool> OccupiedPositions { get { return occupiedPositions; } }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //setting pathCheckScripts using the path check scripts of the block list
        pathCheckScripts = new List<PathCheck>();

        foreach (GameObject g in blockObjects)
        {
            pathCheckScripts.Add(g.GetComponent<PathCheck>());
        }

        //if there is no score manager in the scene create one
        if (GameObject.Find("ScoreManager") == null && GameObject.Find("ScoreManager(Clone)") == null)
        {
            Debug.Log(GameObject.Find("ScoreManager"));
            Instantiate(scoreManagerPrefab);
        }

        //setup the occupied positions dictionary
        occupiedPositions = new Dictionary<string, bool>();
        for (int i = 0; i < 8; i++)
        {
            occupiedPositions.Add("s" + i, false);
            occupiedPositions.Add("l" + i, false);
        }

        //reset values in the occupied positions dictionary if the spots are occupied
        for (int i = 0; i < blockObjects.Count - 1; i++)
        {
            string position = blockObjects[i].GetComponent<BlockMovement>().CurrentPos;

            occupiedPositions[position] = true;
        }

        //set raycast distance for every path check script
        foreach (PathCheck p in pathCheckScripts)
        {
            p.RaycastDistance = pathRaycastDistance;
            p.ValidPath = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //loop through all of the path check scripts and determine if they are valid or not
        for (int i = 0; i < pathCheckScripts.Count; i++)
        {
            //if it hasn't already been checked this cycle, check it
            if (!pathCheckScripts[i].AlreadyChecked)
            {
                //if a valid path is found
                //set already checked to true and reset i 
                //so that the loop will make sure nothing was missed behind it
                if (pathCheckScripts[i].CheckPaths())
                {
                    pathCheckScripts[i].AlreadyChecked = true;
                    i = 0;
                }
            }
        }

        //change all valid paths to the blood material
        //change all invalid paths to the empty path material
        foreach (PathCheck p in pathCheckScripts)
        {
            if (p.ValidPath) p.ChangeToBloodMaterial();
            else p.ChangeToEmptyPathMaterial(); 
        }

        //if the exit piece is valid
        //turn off movement and enable the win canvas
        if (exitPiece.GetComponent<PathCheck>().ValidPath)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            GameObject.Find("WinCanvas").GetComponent<Canvas>().enabled = true;

            TurnOffMovement(); 

            GameObject.Find("MovesRemain").GetComponent<MovesRemaining>().CountingDown = false;

            GameObject.Find("PauseCanvas").GetComponent<PauseMenu>().AbleToPause = false;
        }
    }

    private void LateUpdate()
    {
        //At the end of the frame mark every path as invalid
        foreach (PathCheck p in pathCheckScripts)
        {
            p.ValidPath = false;
            p.AlreadyChecked = false;
        }
    }

    /// <summary>
    /// Go to the next level in the build order
    /// </summary>
    public void NextLevel()
    {
        //if this scene is a level
        //call end level on score manager so the total score is updated
        if (SceneManager.GetActiveScene().name.Substring(0, 5) == "Level")
        {
            try
            {
                GameObject.Find("ScoreManager").GetComponent<ScoreManagement>().EndLevel();
            }
            catch (Exception e)
            {
                GameObject.Find("ScoreManager(Clone)").GetComponent<ScoreManagement>().EndLevel();
            }
        }

        //load the next scene in the build order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Restart the level by reloading it
    /// </summary>
    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    /// <summary>
    /// Turn off all block movement
    /// </summary>
    public void TurnOffMovement()
    {
        for (int i = 0; i < blockObjects.Count - 1; i++)
        {
            blockObjects[i].GetComponent<BlockMovement>().enabled = false;
        }
    }

    /// <summary>
    /// Turn on all block movement
    /// </summary>
    public void TurnOnMovement()
    {
        for (int i = 0; i < blockObjects.Count - 1; i++)
        {
            blockObjects[i].GetComponent<BlockMovement>().enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private List<GameObject> blockObjects;
    private List<PathCheck> pathCheckScripts;
    [SerializeField]
    private int pathRaycastDistance;
    [SerializeField]
    private GameObject exitPiece;
    [SerializeField]
    private GameObject scoreManagerPrefab;

    private Dictionary<string, bool> occupiedPositions; 
    #endregion

    #region Properties
    public List<GameObject> BlockObjects { get { return blockObjects; } }
    public Dictionary<string, bool> OccupiedPositions { get { return occupiedPositions; } }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        pathCheckScripts = new List<PathCheck>();

        foreach (GameObject g in blockObjects)
        {
            pathCheckScripts.Add(g.GetComponent<PathCheck>());
        }

        //pathRaycastDistance = 50;

        if (GameObject.Find("ScoreManager") == null && GameObject.Find("ScoreManager(Clone)") == null)
        {
            Debug.Log(GameObject.Find("ScoreManager"));
            Instantiate(scoreManagerPrefab);
        }

        occupiedPositions = new Dictionary<string, bool>();
        for (int i = 0; i < 8; i++)
        {
            occupiedPositions.Add("s" + i, false);
            occupiedPositions.Add("l" + i, false);
        }

        for (int i = 0; i < blockObjects.Count - 1; i++)
        {
            string position = blockObjects[i].GetComponent<BlockMovement>().CurrentPos;

            occupiedPositions[position] = true;
        }

        foreach (PathCheck p in pathCheckScripts)
        {
            p.RaycastDistance = pathRaycastDistance;
            p.ValidPath = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PathCheck p in pathCheckScripts)
        {
            p.CheckPaths();
        }

        if (exitPiece.GetComponent<PathCheck>().ValidPath)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            GameObject.Find("WinCanvas").GetComponent<Canvas>().enabled = true;

            TurnOffMovement(); 

            GameObject.Find("MovesRemain").GetComponent<MovesRemaining>().CountingDown = false;

            GameObject.Find("PauseCanvas").GetComponent<PauseMenu>().ableToPause = false;
        }
    }

    private void LateUpdate()
    {
        foreach (PathCheck p in pathCheckScripts)
        {
            p.ValidPath = false;
        }
    }

    public void NextLevel()
    {
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void TurnOffMovement()
    {
        for (int i = 0; i < blockObjects.Count - 1; i++)
        {
            blockObjects[i].GetComponent<BlockMovement>().enabled = false;
        }
    }

    public void TurnOnMovement()
    {
        for (int i = 0; i < blockObjects.Count - 1; i++)
        {
            blockObjects[i].GetComponent<BlockMovement>().enabled = true;
        }
    }
}

using UnityEngine;
using System.Collections.Generic;
using System;

public class BlockMovement : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool circular;
    private bool clicked;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int size;
    [SerializeField]
    private GameObject[] snapLocations;
    private Vector3 mousePreviousPosition;
    public GameObject movesObject;
    [SerializeField]
    private int position; 
    private string currentPos;
    [SerializeField]
    private bool movingClockwise; 
    #endregion

    #region Properties
    public string CurrentPos { get { return currentPos; } }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        mousePreviousPosition = Input.mousePosition;

        snapLocations = GameObject.FindGameObjectsWithTag("SnapLocation");

        if (size == 0)
        {
            currentPos = "s" + position;
        }
        else
        {
            currentPos = "l" + position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (circular)
        {
            AngularMovement(); 
        }  
        else
        {
            StraightMovement(); 
        }
    }

    public void AngularMovement()
    {
        if (clicked)
        {
            Vector3 centerToBlock = (transform.position - Vector3.zero).normalized;
            Vector3 right = new Vector3(centerToBlock.z, centerToBlock.y, -centerToBlock.x);
            Vector3 mouseCurrentPosition = Input.mousePosition;
            mouseCurrentPosition = Camera.main.ScreenToWorldPoint(mouseCurrentPosition); 
            float dotProduct = Vector3.Dot(gameObject.transform.right, mouseCurrentPosition);
            float mag = (mouseCurrentPosition - gameObject.transform.position).magnitude; 

            Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + gameObject.transform.right);
            Debug.DrawLine(Vector3.zero, mouseCurrentPosition); 

            if (dotProduct < 0)
            {
                Vector3 rotation = gameObject.transform.eulerAngles;
                rotation.z += speed;// * Time.deltaTime;
                gameObject.transform.eulerAngles = rotation;

                if (gameObject.transform.eulerAngles.z > 360)
                {
                    Vector3 fixedRotation = gameObject.transform.eulerAngles;
                    fixedRotation.z = fixedRotation.z - 360;
                    gameObject.transform.eulerAngles = fixedRotation;
                }

                //movingClockwise = false; 
            }
            else
            {
                Vector3 rotation = gameObject.transform.eulerAngles;
                rotation.z -= speed;
                gameObject.transform.eulerAngles = rotation;

                if (gameObject.transform.eulerAngles.z < 0)
                {
                    Vector3 fixedRotation = gameObject.transform.eulerAngles;
                    fixedRotation.z = 360 + fixedRotation.z;
                    gameObject.transform.eulerAngles = fixedRotation;
                }

                //movingClockwise = true;
            }
            mousePreviousPosition = mouseCurrentPosition; 
        }
    }

    public void StraightMovement()
    {
        if (clicked)
        {
            
        }
    }

    void OnMouseDown()
    {
        clicked = true;
        mousePreviousPosition = Input.mousePosition; 
    }

    void OnMouseUp()
    {
        clicked = false;

        if (!GameObject.Find("PauseCanvas").GetComponent<PauseMenu>().paused)
        {
            movesObject.GetComponent<MovesRemaining>().RemoveMoves();
        }

        List<float> rotationDiffs = new List<float>(snapLocations.Length); 

        for (int i = 0; i < snapLocations.Length; i++)
        {
            rotationDiffs.Add(Mathf.Abs(transform.eulerAngles.z - snapLocations[i].transform.eulerAngles.z)); 
        }

        int smallestDiffInd = 0;

        for (int i = 1; i < snapLocations.Length; i++)
        {
            if (rotationDiffs[i] < rotationDiffs[smallestDiffInd])
            {
                smallestDiffInd = i;
            }
        }

        string prevPos = currentPos; 

        position = smallestDiffInd;

        SetCurrentPos();

        if (Int32.Parse(prevPos.Substring(1,1)) > position) movingClockwise = true;
        else movingClockwise = false;

        while (Camera.main.GetComponent<GameController>().OccupiedPositions[currentPos])
        {
            if (movingClockwise) smallestDiffInd++;
            else smallestDiffInd--;

            if (smallestDiffInd > 7) smallestDiffInd = 0;
            else if (smallestDiffInd < 0) smallestDiffInd = 8; 

            position = smallestDiffInd;

            SetCurrentPos();

            Debug.Log(currentPos); 
        }

        Camera.main.GetComponent<GameController>().OccupiedPositions[currentPos] = true;
        Camera.main.GetComponent<GameController>().OccupiedPositions[prevPos] = false;

        if (Mathf.Abs(transform.eulerAngles.z - 360) < rotationDiffs[smallestDiffInd])
        {
            Vector3 rotation = gameObject.transform.eulerAngles;
            rotation.z = 0;
            gameObject.transform.eulerAngles = rotation;
        }
        else
        {
            Vector3 rotation = gameObject.transform.eulerAngles;
            rotation.z = snapLocations[smallestDiffInd].transform.eulerAngles.z;
            gameObject.transform.eulerAngles = rotation;
        }
    }

    private void SetCurrentPos()
    {
        if (size == 0)
        {
            currentPos = "s" + position;
        }
        else
        {
            currentPos = "l" + position;
        }
    }
}

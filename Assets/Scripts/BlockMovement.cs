using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Controls movement of blocks in the scene
/// 
/// author: Trenton Plager tlp6760@rit.edu
/// </summary>
public class BlockMovement : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool circular;                      //is this block circular?
    private bool clicked;                       //has this block been clicked?
    [SerializeField]
    private float speed;                        //the speed to move the block with
    [SerializeField]
    private int size;                           //is the block large (1) or small (0)
    [SerializeField]
    private GameObject[] snapLocations;         //a list of snap locations present in the scene
    [SerializeField]
    private GameObject movesObject;             //the moves object
    [SerializeField]
    private int position;                       //the position of the block; starts at 0 at the top of the circle and 
                                                //moves counter-clockwise by 45 degree increments, ending at 7 at 315 degrees
    private string currentPos;                  //the current position of the block in a string format; includes the size
    [SerializeField]
    private bool movingClockwise;               //is the block moving clockwise?
    #endregion

    #region Properties
    public string CurrentPos { get { return currentPos; } }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //initialization
        clicked = false;

        snapLocations = GameObject.FindGameObjectsWithTag("SnapLocation");

        SetCurrentPos(); 
    }

    // Update is called once per frame
    void Update()
    {
        //if (circular)
        //{
        //    AngularMovement(); 
        //}  
        //else
        //{
        //    StraightMovement(); 
        //}

        AngularMovement(); 
    }

    /// <summary>
    /// Moves circular blocks around the circle
    /// </summary>
    public void AngularMovement()
    {
        //if the block has been clicked
        //determine if it needs to be moved
        if (clicked)
        {
            //set mouse current position to the mouse's current position
            //then convert it to screen space
            Vector3 mouseCurrentPosition = Input.mousePosition;
            mouseCurrentPosition = Camera.main.ScreenToWorldPoint(mouseCurrentPosition); 

            //calculate the dot product to determine which side of the block the mouse is on
            float dotProduct = Vector3.Dot(gameObject.transform.right, mouseCurrentPosition);

            //Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + gameObject.transform.right);
            //Debug.DrawLine(Vector3.zero, mouseCurrentPosition); 

            //if the dot product is less than 0
            //the mouse is to the right of the block
            if (dotProduct < 0)
            {
                //update the rotation of the block 
                //so it moves around the circle to the right
                Vector3 rotation = gameObject.transform.eulerAngles;
                rotation.z += speed;

                //if the rotation is greater than 360 subtract 360
                if (rotation.z > 360) rotation.z = rotation.z - 360;

                gameObject.transform.eulerAngles = rotation;
            }
            //if the dot product is greater than 0 
            //the mouse is to the left of the block
            else
            {
                //update the rotation of the block
                //so it moves around the circle to the left
                Vector3 rotation = gameObject.transform.eulerAngles;
                rotation.z -= speed;

                //if the rotation is less than 0 add 360
                if (rotation.z < 0) rotation.z = 360 + rotation.z;

                gameObject.transform.eulerAngles = rotation;
            }
        }
    }

    //public void StraightMovement()
    //{
    //    if (clicked)
    //    {
            
    //    }
    //}

    /// <summary>
    /// If the mouse has been pressed down on this block set clicked to true
    /// </summary>
    void OnMouseDown()
    {
        clicked = true;

        try
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayStoneGrinding();
        }
        catch (Exception e)
        {
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().PlayStoneGrinding();
        }
    }

    /// <summary>
    /// If the mouse has been released update moves and determine where to move the block to
    /// </summary>
    void OnMouseUp()
    {
        try
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().StopStoneGrinding();
        }
        catch (Exception e)
        {
            GameObject.Find("AudioManager(Clone)").GetComponent<AudioManager>().StopStoneGrinding();
        }

        //reset clicked
        clicked = false;

        //update the remaining moves if not paused
        if (!GameObject.Find("PauseCanvas").GetComponent<PauseMenu>().Paused)
        {
            movesObject.GetComponent<MovesRemaining>().RemoveMoves();
        }

        //create a new list of the differences between the current rotation
        //and the snap locations' rotations
        List<float> rotationDiffs = new List<float>(snapLocations.Length); 

        //add the differences to the list
        for (int i = 0; i < snapLocations.Length; i++)
        {
            rotationDiffs.Add(Mathf.Abs(transform.eulerAngles.z - snapLocations[i].transform.eulerAngles.z)); 
        }

        //determine which is the smallest difference
        int smallestDiffInd = 0;

        for (int i = 1; i < snapLocations.Length; i++)
        {
            if (rotationDiffs[i] < rotationDiffs[smallestDiffInd])
            {
                smallestDiffInd = i;
            }
        }

        //update the current position and position fields
        //to the actual current position of the block
        string prevPos = currentPos; 

        position = smallestDiffInd;

        SetCurrentPos();
        
        //determine if the block was moving clockwise or not
        if (Int32.Parse(prevPos.Substring(1,1)) > position) movingClockwise = true;
        else movingClockwise = false;

        //loop through until an unoccupied spot is found
        while (Camera.main.GetComponent<GameController>().OccupiedPositions[currentPos])
        {
            //if this spot is occuped change the smallest difference index
            if (movingClockwise) smallestDiffInd++;
            else smallestDiffInd--;

            //reset it if it overflows either way
            if (smallestDiffInd > 7) smallestDiffInd = 0;
            else if (smallestDiffInd < 0) smallestDiffInd = 8; 

            //set position and current position
            position = smallestDiffInd;

            SetCurrentPos();
        }

        //Update the occuped positions dictionary with the new positions
        Camera.main.GetComponent<GameController>().OccupiedPositions[currentPos] = true;
        Camera.main.GetComponent<GameController>().OccupiedPositions[prevPos] = false;

        //Update the actual position of the block
        //so that it snaps to the nearest snap location
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

    /// <summary>
    /// Set the current position depending on size and position
    /// </summary>
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

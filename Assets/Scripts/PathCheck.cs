using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if paths are valid around the board
/// 
/// author: Trenton Plager tlp6760@rit.edu
/// </summary>
public class PathCheck : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool[] validPaths = new bool[4];    //an array indicating where this path can go
    public string layerMaskName;                //the name of the current layer
    private string layerUpMaskName;             //the name of the layer above this one
    private string layerDownMaskName;           //the name of the layer below this one
    [SerializeField]
    private bool validPath;                     //is this path valid?
    [SerializeField]
    private bool centerPath;                    //is this the center path?
    [SerializeField]
    private int raycastDistance;                //how far should the path raycast

    private bool alreadyChecked;                //has this path already been checked?
    #endregion

    #region Properties
    public bool[] ValidPaths { get { return validPaths; } }
    public bool ValidPath { get { return validPath; } set { validPath = value; } }
    public bool IsCenter { get { return centerPath; } }
    public int RaycastDistance
    {
        get { return raycastDistance; }
        set { raycastDistance = value; }
    }
    public bool AlreadyChecked
    {
        get { return alreadyChecked; }
        set { alreadyChecked = value; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //If the layer is less than 11 assign layer up name
        if (gameObject.layer < 11)
        {
            layerUpMaskName = LayerMask.LayerToName(gameObject.layer + 1);
        }

        //assign the layer down and layer name according to the name of the layer
        layerDownMaskName = LayerMask.LayerToName(gameObject.layer - 1);
        layerMaskName = LayerMask.LayerToName(gameObject.layer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Check if the path in front of this block is valid
    /// </summary>
    /// <returns>Whether there is a valid path or not</returns>
    private bool CheckPathsInFront()
    {
        //Raycast to determine if something is in front or not
        RaycastHit hit; 
        bool isSomethingInFront = Physics.Raycast(gameObject.transform.position, gameObject.transform.up, out hit, raycastDistance, LayerMask.GetMask(layerUpMaskName));
        //if (isSomethingInFront) { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (gameObject.transform.up), Color.red); }
        //else { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (gameObject.transform.up), Color.black); }

        //if something is in front
        //and that something has a path check object that is open behind it and that path is valid
        //set this path to valid and return true
        if (isSomethingInFront)
        {
            if (hit.collider.gameObject.GetComponent<PathCheck>().ValidPaths[2] && hit.collider.gameObject.GetComponent<PathCheck>().ValidPath)
            {
                validPath = true;

                return true; 
            }
        }

        return false; 
    }

    /// <summary>
    /// Check if the path behind this block is valid
    /// </summary>
    /// <returns>Whether there is a valid path or not</returns>
    private bool CheckPathsBehind()
    {
        //Raycast to determine if something is behind or not
        RaycastHit hit;
        bool isSomethingBehind = Physics.Raycast(gameObject.transform.position, gameObject.transform.up, out hit, raycastDistance, LayerMask.GetMask(layerDownMaskName));
        //if (isSomethingBehind) { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (gameObject.transform.up * raycastDistance), Color.red); }
        //else { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (gameObject.transform.up * raycastDistance), Color.black); }

        //if something is behind
        //and that something has a path check object that is open in front and that path is valid
        //set this path to valid and return true
        if (isSomethingBehind)
        {
            if (hit.collider.gameObject.GetComponent<PathCheck>().ValidPaths[0] && hit.collider.gameObject.GetComponent<PathCheck>().ValidPath)
            {
                validPath = true;

                return true;
            }
        }

        return false; 
    }

    /// <summary>
    /// Check if the path to the left of this block is valid
    /// </summary>
    /// <returns>Whether there is a valid path or not</returns>
    private bool CheckPathsLeft()
    {
        RaycastHit hit;

        //the current z rotation of this object in an euler representation
        Quaternion rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z + 45);

        //Raycast to determine if something is to the left or not
        bool isSomethingLeft = Physics.Raycast(gameObject.transform.position, rotation * gameObject.transform.up, out hit, raycastDistance, LayerMask.GetMask(layerMaskName));
        //if (isSomethingLeft) { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (rotation * gameObject.transform.up * raycastDistance), Color.red); }
        //else { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (rotation * gameObject.transform.up * raycastDistance), Color.black); }

        //if something is to the left
        //and that something has a path check object that is open to the right and that path is valid
        //set this path to valid and return true
        if (isSomethingLeft)
        {
            if (hit.collider.gameObject.GetComponent<PathCheck>().ValidPaths[1] && hit.collider.gameObject.GetComponent<PathCheck>().ValidPath)
            {
                validPath = true;

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Check if the path to the right of this block is valid
    /// </summary>
    /// <returns>Whether there is a valid path or not</returns>
    private bool CheckPathsRight()
    {
        RaycastHit hit;

        //the current z rotation of this object in an euler representation
        Quaternion rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z - 45);

        //Raycast to determine if something is to the right or not
        bool isSomethingRight = Physics.Raycast(gameObject.transform.position, rotation * gameObject.transform.up, out hit, raycastDistance, LayerMask.GetMask(layerMaskName));
        //if (isSomethingRight) { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (rotation * gameObject.transform.up), Color.red); }
        //else { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (rotation * gameObject.transform.up), Color.black); }

        //if something is to the right
        //and that something has a path check object that is open to the left and that path is valid
        //set this path to valid and return true
        if (isSomethingRight)
        {
            if (hit.collider.gameObject.GetComponent<PathCheck>().ValidPaths[3] && hit.collider.gameObject.GetComponent<PathCheck>().ValidPath)
            {
                validPath = true;

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Change the current path to red
    /// </summary>
    public void ChangeToBloodMaterial()
    {
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer s in spriteRenderers)
        {
            if (s.gameObject.tag == "Path")
            {
                s.color = Color.red;
            }
        }
    }

    /// <summary>
    /// Change the current path to white
    /// </summary>
    public void ChangeToEmptyPathMaterial()
    {
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer s in spriteRenderers)
        {
            if (s.gameObject.tag == "Path")
            {
                s.color = Color.white;
            }
        }
    }

    /// <summary>
    /// Check all the paths that are open on this object
    /// If one path is valid return true because it doesn't really matter which one is valid
    /// as long as the path itself is marked valid
    /// </summary>
    /// <returns>A boolean indicating whether this path is valid or not</returns>
    public bool CheckPaths()
    {
        //if this is the center path don't check anything
        if (layerMaskName == "CenterLayer")
        {
            validPath = true;
            return false;
        }

        //if this block has an opening in the front 
        //check paths in front
        if (validPaths[0])
        {
            //if there is a valid path return true
            if (CheckPathsInFront()) return true; 
        }

        //if this path has an opening to the right 
        //check paths to the right
        if (validPaths[1])
        {
            //if there is a valid path return true
            if (CheckPathsRight()) return true;
        }

        //if this path has an opening behind it 
        //check paths behind it
        if (validPaths[2])
        {
            //if there is a valid path return true
            if (CheckPathsBehind()) return true;
        }

        //if this path has an opening to the left
        //check paths to the left
        if (validPaths[3])
        {
            //if there is a valid path return true
            if (CheckPathsLeft()) return true;
        }

        //if (validPath)
        //{
        //    ChangeToBloodMaterial();
        //}
        //else
        //{
        //    ChangeToEmptyPathMaterial();
        //}

        return false; 
    }
}

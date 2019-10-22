﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCheck : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool[] validPaths = new bool[4]; //an array indicating where this path can go
    public string layerMaskName;
    private string layerUpMaskName;
    private string layerDownMaskName; 
    //[SerializeField]
    //private Material bloodMaterial;
    //[SerializeField]
    //private Material pathMaterial;
    [SerializeField]
    private bool validPath;
    [SerializeField]
    private bool centerPath;
    [SerializeField]
    private int raycastDistance; 
    #endregion

    public bool[] ValidPaths { get { return validPaths; } }
    public bool ValidPath { get { return validPath; } set { validPath = value; } }
    public bool IsCenter { get { return centerPath; } }
    public int RaycastDistance
    {
        get { return raycastDistance; }
        set { raycastDistance = value; }
    }
        
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.layer < 11)
        {
            layerUpMaskName = LayerMask.LayerToName(gameObject.layer + 1);
        }
        layerDownMaskName = LayerMask.LayerToName(gameObject.layer - 1);
        layerMaskName = LayerMask.LayerToName(gameObject.layer);

        //validPath = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (layerMaskName == "CenterLayer")
        //{
        //    validPath = true;
        //    return;
        //}

        //if (validPaths[0])
        //{
        //    CheckPathsInFront();
        //}
        //if (validPaths[1])
        //{
        //    CheckPathsRight();
        //}
        //if (validPaths[2])
        //{
        //    CheckPathsBehind();
        //}
        //if (validPaths[3])
        //{
        //    CheckPathsLeft();
        //}

        //if (validPath)
        //{
        //    ChangeToBloodMaterial();
        //}
        //else
        //{
        //    ChangeToEmptyPathMaterial();
        //}
    }

    private void CheckPathsInFront()
    {
        RaycastHit hit; 
        bool isSomethingInFront = Physics.Raycast(gameObject.transform.position, gameObject.transform.up, out hit, raycastDistance, LayerMask.GetMask(layerUpMaskName));
        //if (isSomethingInFront) { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (gameObject.transform.up), Color.red); }
        //else { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (gameObject.transform.up), Color.black); }

        if (isSomethingInFront)
        {
            if (hit.collider.gameObject.GetComponent<PathCheck>().ValidPaths[2] && hit.collider.gameObject.GetComponent<PathCheck>().ValidPath)
            {
                validPath = true;
            }
        }
        //else validPath = false;
    }

    private void CheckPathsBehind()
    {
        RaycastHit hit;
        bool isSomethingBehind = Physics.Raycast(gameObject.transform.position, gameObject.transform.up, out hit, raycastDistance, LayerMask.GetMask(layerDownMaskName));
        //if (isSomethingBehind) { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (gameObject.transform.up * raycastDistance), Color.red); }
        //else { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (gameObject.transform.up * raycastDistance), Color.black); }

        if (isSomethingBehind)
        {
            if (hit.collider.gameObject.GetComponent<PathCheck>().ValidPaths[0] && hit.collider.gameObject.GetComponent<PathCheck>().ValidPath)
            {
                validPath = true;
            }
        }
    }

    private void CheckPathsLeft()
    {
        RaycastHit hit;
        Quaternion rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z + 45);

        bool isSomethingLeft = Physics.Raycast(gameObject.transform.position, rotation * gameObject.transform.up, out hit, raycastDistance, LayerMask.GetMask(layerMaskName));
        if (isSomethingLeft) { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (rotation * gameObject.transform.up * raycastDistance), Color.red); }
        else { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (rotation * gameObject.transform.up * raycastDistance), Color.black); }

        if (isSomethingLeft)
        {
            Debug.Log(gameObject.name + ": " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<PathCheck>().ValidPaths[1] && hit.collider.gameObject.GetComponent<PathCheck>().ValidPath)
            {
                Debug.Log("Hit valid");
                validPath = true;
            }
        }
    }

    private void CheckPathsRight()
    {
        RaycastHit hit;

        Quaternion rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z - 45);

        bool isSomethingRight = Physics.Raycast(gameObject.transform.position, rotation * gameObject.transform.up, out hit, raycastDistance, LayerMask.GetMask(layerMaskName));
        //if (isSomethingRight) { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (rotation * gameObject.transform.up), Color.red); }
        //else { Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (rotation * gameObject.transform.up), Color.black); }

        if (isSomethingRight)
        {
            if (hit.collider.gameObject.GetComponent<PathCheck>().ValidPaths[3] && hit.collider.gameObject.GetComponent<PathCheck>().ValidPath)
            {
                validPath = true;
            }
        }
    }

    public void ChangeToBloodMaterial()
    {
        //MeshRenderer[] blockRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();

        //foreach (MeshRenderer m in blockRenderers)
        //{
        //    m.material = bloodMaterial;
        //}

        foreach (SpriteRenderer s in spriteRenderers)
        {
            if (s.gameObject.tag == "Path")
            {
                s.color = Color.red;
            }
        }
    }

    public void ChangeToEmptyPathMaterial()
    {
        //MeshRenderer[] blockRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();

        //foreach (MeshRenderer m in blockRenderers)
        //{
        //    m.material = pathMaterial;
        //}

        foreach (SpriteRenderer s in spriteRenderers)
        {
            if (s.gameObject.tag == "Path")
            {
                s.color = Color.white;
            }
        }
    }

    public void CheckPaths()
    {
        if (layerMaskName == "CenterLayer")
        {
            validPath = true;
            return;
        }

        if (validPaths[0])
        {
            CheckPathsInFront();
        }
        if (validPaths[1])
        {
            CheckPathsRight();
        }
        if (validPaths[2])
        {
            CheckPathsBehind();
        }
        if (validPaths[3])
        {
            CheckPathsLeft();
        }

        if (validPath)
        {
            ChangeToBloodMaterial();
        }
        else
        {
            ChangeToEmptyPathMaterial();
        }
    }
}

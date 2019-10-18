﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private List<GameObject> blockObjects;
    private List<PathCheck> pathCheckScripts;
    [SerializeField]
    private int pathRaycastDistance; 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        pathCheckScripts = new List<PathCheck>();

        foreach (GameObject g in blockObjects)
        {
            pathCheckScripts.Add(g.GetComponent<PathCheck>()); 
        }

        pathRaycastDistance = 50;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PathCheck p in pathCheckScripts)
        {
            if (!p.IsCenter) p.ValidPath = false;
            p.RaycastDistance = pathRaycastDistance;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        mousePreviousPosition = Input.mousePosition; 
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
            Vector3 mouseCurrentPosition; 
            //if (mousePreviousPosition.x < mouseCurrentPosition.x)
            {
                //Quaternion rotation = gameObject.transform.eulerAngles;
                //rotation.z -= speed;
                //gameObject.transform.rotation = rotation; 
            }
        }
        //mousePreviousPosition = mouseCurrentPosition; 
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
    }
}

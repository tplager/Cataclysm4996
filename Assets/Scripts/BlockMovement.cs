using UnityEngine;
using System.Collections.Generic; 

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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        mousePreviousPosition = Input.mousePosition;

        snapLocations = GameObject.FindGameObjectsWithTag("SnapLocation");
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

        movesObject.GetComponent<MovesRemaining>().RemoveMoves();

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public Text tutorial;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("Hello, welcome to Cataclysm 4996", 5.0f);
        //StartCoroutine("Click and Drag on top of a piece to move it", 5.0f);
        //StartCoroutine("Make a path from the red center to the white piece outside of the circle to complete the level", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        tutorial.text = message;
        tutorial.enabled = true;
        yield return new WaitForSeconds(delay);
        tutorial.enabled = false;
    }
}

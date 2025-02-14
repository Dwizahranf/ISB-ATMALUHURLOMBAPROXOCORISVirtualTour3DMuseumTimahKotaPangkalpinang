using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlight : MonoBehaviour
{
     public Material highlightMaterial;
    private Material originalMaterial;
    private bool isHighlighted = false;
    private int touchCount = 0;

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    private void OnMouseDown()
    {
        touchCount++;

        if (touchCount == 1)
        {
            HighlightObject();
        }
        else if (touchCount == 2)
        {
            InteractWithObject();
        }
    }

    private void HighlightObject()
    {
        GetComponent<Renderer>().material = highlightMaterial;
        isHighlighted = true;
    }

    private void InteractWithObject()
    {
        // Implement interaction logic here
        Debug.Log("Interacting with object: " + gameObject.name);

        // Reset the object to its original state
        ResetObject();
    }

    private void ResetObject()
    {
        // Reset the material to its original state
        GetComponent<Renderer>().material = originalMaterial;

        // Reset touch count
        touchCount = 0;
        isHighlighted = false;
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelScript : MonoBehaviour, IPointerDownHandler
{
    public FirstPersonController firstPersonController; // Reference to the FirstPersonController script

    // Implement the interface methods for pointer down and pointer up events
    public void OnPointerDown(PointerEventData eventData)
    {
        // Disable rotation when the panel is clicked or interacted with
        if (firstPersonController != null)
        {
            firstPersonController.DisableLookAround();
        }
    }

    
}
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInputBlocker : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        // Check if the pause menu is active
        if (isPaused)
        {
            // Block touch input when the pause menu is active
            BlockTouchInput();
        }
    }

    public void SetPausedState(bool paused)
    {
        isPaused = paused;
    }

    private void BlockTouchInput()
    {
        // Check if any touch input is detected
        if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch touch = Input.GetTouch(0);
            
            // Check if the touch is over a UI element
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                // UI element is being touched, prevent interaction
                return;
            }
        }

        // Allow touch input for non-UI elements
    }
}

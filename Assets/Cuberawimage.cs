using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Cuberawimage : MonoBehaviour
{
    public GameObject haiText;
    public GameObject haiSprite;
    public GameObject haiPanel;
    public RawImage haiRawImage; // Reference to the Raw Image component
    public Button closeButton;
    public Button soundButton;
    public float interactionDistance = 3f; // Interaction distance
    public float tapThreshold = 0.2f; // Time in seconds to detect a tap

    private FirstPersonController firstPersonController; // Reference to the FirstPersonController script
    private bool isTouching = false;
    private float touchStartTime;

    // Static variable to track if any object is currently being interacted with
    private static bool isAnyObjectInteracting = false;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        // Handle touch input for interaction
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isTouching = true;
                    touchStartTime = Time.time;
                    break;

                case TouchPhase.Ended:
                    if (isTouching && Time.time - touchStartTime <= tapThreshold)
                    {
                        if (!IsPointerOverUIElement(touch.position))
                        {
                            // Detect if the tap was on this object
                            Ray ray = Camera.main.ScreenPointToRay(touch.position);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                            {
                                OnTap();
                            }
                        }
                        else
                        {
                            // Handle UI interaction
                            HandleUIInteraction(touch.position);
                        }
                    }
                    isTouching = false;
                    break;

                case TouchPhase.Canceled:
                    isTouching = false;
                    break;
            }
        }
    }

    // When the object is tapped
    void OnTap()
    {
        // Check interaction distance and if any object is already being interacted with
        if (IsWithinInteractionDistance() && !isAnyObjectInteracting)
        {
            // Set the static variable to indicate this object is being interacted with
            isAnyObjectInteracting = true;

            // Show "Hai" text and "Close" button when the cube is clicked
            ShowTextAndButton();

            // Disable player movement and rotation when the object is clicked
            firstPersonController.DisableMovement();
            firstPersonController.DisableLookAround();
        }
    }

    bool IsWithinInteractionDistance()
    {
        // Check the distance between the player and the object
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        return distance <= interactionDistance;
    }

    void ShowTextAndButton()
    {
        haiText.SetActive(true);
        haiSprite.SetActive(true);
        haiPanel.SetActive(true);
        haiRawImage.enabled = true; // Show the Raw Image
        closeButton.gameObject.SetActive(true);
        soundButton.gameObject.SetActive(true);
    }

    void HideTextAndButton()
    {
        haiText.SetActive(false);
        haiSprite.SetActive(false);
        haiPanel.SetActive(false);
        haiRawImage.enabled = false; // Hide the Raw Image
        closeButton.gameObject.SetActive(false);
        soundButton.gameObject.SetActive(false);
    }

    void CloseText()
    {
        HideTextAndButton();

        // Enable player movement and rotation when the "Close" button is pressed
        firstPersonController.EnableMovement();
        firstPersonController.EnableLookAround();

        // Reset the static variable to indicate no object is being interacted with
        isAnyObjectInteracting = false;
    }

    void Initialize()
    {
        // Get reference to the FirstPersonController script
        firstPersonController = FindObjectOfType<FirstPersonController>();

        // Hide "Hai" text and "Close" button at the start of the game
        HideTextAndButton();

        // Subscribe to the onClick event of the close button programmatically
        closeButton.onClick.AddListener(CloseText);
    }

    private bool IsPointerOverUIElement(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = touchPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    private void HandleUIInteraction(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = touchPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            // Check if the result is a Button and invoke its onClick event
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke();
                break;
            }
        }
    }
}

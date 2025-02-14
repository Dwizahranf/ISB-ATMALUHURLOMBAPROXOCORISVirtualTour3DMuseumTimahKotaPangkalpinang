using UnityEngine;
using UnityEngine.UI;

public class CubeInter : MonoBehaviour
{
    public GameObject haiText;
    public GameObject haiSprite;
    public GameObject haiPanel;
    public Button closeButton;
    public float interactionDistance = 3f; // Interaction distance
    public float tapThreshold = 0.3f; // Time in seconds to detect a tap

    private FirstPersonController firstPersonController; // Reference to the FirstPersonController script
    private bool isTouching = false;
    private float touchStartTime;

    // Static variable to track if any object is currently being interacted with
    private static bool isAnyObjectInteracting = false;

    void Awake()
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
                        // Detect if the tap was on this object
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                        {
                            OnTap();
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
        closeButton.gameObject.SetActive(true);
    }

    void HideTextAndButton()
    {
        haiText.SetActive(false);
        haiSprite.SetActive(false);
        haiPanel.SetActive(false);
        closeButton.gameObject.SetActive(false);
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
        // Find and assign the FirstPersonController script
        firstPersonController = FindObjectOfType<FirstPersonController>();

        // Hide the text and buttons initially
        HideTextAndButton();

        // Subscribe to the onClick event of the close button
        closeButton.onClick.AddListener(CloseText);
    }
}

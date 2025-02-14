using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // References
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController characterController;

    // Player settings
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveInputDeadZone;
    [SerializeField] private float gravityMultiplier; // New gravity multiplier

    // Touch detection
    private int leftFingerId, rightFingerId;
    private float halfScreenWidth;

    // Camera control
    private Vector2 lookInput;
    private float cameraPitch;

    // Player movement
    private Vector2 moveTouchStartPosition;
    private Vector2 moveInput;

    // Flags to control movement and rotation
    private bool canMove = true;
    private bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        // id = -1 means the finger is not being tracked
        leftFingerId = -1;
        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;

        // calculate the movement input dead zone
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }

    // Update is called once per frame
    void Update()
    {
        // Handles input
        GetTouchInput();

        if (canRotate && rightFingerId != -1) // Check if rotation is enabled
        {
            // Only look around if rotation is enabled and the right finger is being tracked
            LookAround();
        }

        if (canMove && leftFingerId != -1) // Check if movement is enabled
        {
            // Only move if movement is enabled and the left finger is being tracked
            Move();
        }

        // Apply gravity
        ApplyGravity();
    }

    void GetTouchInput()
    {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        // Start tracking the left finger if it was not previously being tracked
                        leftFingerId = t.fingerId;
                        // Set the start position for the movement control finger
                        moveTouchStartPosition = t.position;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the right finger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (t.fingerId == leftFingerId)
                    {
                        // Stop tracking the left finger
                        leftFingerId = -1;
                        moveInput = Vector2.zero;
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                        lookInput = Vector2.zero;
                    }
                    break;
                case TouchPhase.Moved:
                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerId)
                    {
                        // calculating the position delta from the start position
                        moveInput = t.position - moveTouchStartPosition;

                        // Only process movement if the touch has moved past a small threshold
                        if (moveInput.sqrMagnitude > moveInputDeadZone)
                        {
                            moveInput = moveInput.normalized;
                        }
                        else
                        {
                            moveInput = Vector2.zero;
                        }
                    }
                    break;
            }
        }
    }

    void LookAround()
    {
        if (canRotate)
        {
            // Vertical (pitch) rotation
            cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

            // Horizontal (yaw) rotation
            float adjustedInputX = lookInput.x;
            transform.Rotate(transform.up, adjustedInputX);
        }
    }

    void Move()
    {
        if (moveInput != Vector2.zero)
        {
            // Multiply the normalized direction by the speed
            Vector2 movementDirection = moveInput * moveSpeed * Time.deltaTime;
            // Move relatively to the local transform's direction
            characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
        }
    }

    void ApplyGravity()
    {
        // Apply gravity to the character controller
        if (!characterController.isGrounded)
        {
            Vector3 gravityVector = Physics.gravity * gravityMultiplier;
            characterController.Move(gravityVector * Time.deltaTime);
        }
    }

    // Method to disable rotation
    public void DisableLookAround()
    {
        Debug.Log("Rotation Disabled");
        canRotate = false;
    }

    // Method to enable rotation
    public void EnableLookAround()
    {
        Debug.Log("Rotation Enabled");
        canRotate = true;
    }

    // Method to disable movement
    public void DisableMovement()
    {
        Debug.Log("Movement Disabled");
        canMove = false;
    }

    // Method to enable movement
    public void EnableMovement()
    {
        Debug.Log("Movement Enabled");
        canMove = true;
    }
}

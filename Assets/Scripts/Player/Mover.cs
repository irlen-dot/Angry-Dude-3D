using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float slowSpeed = 2f;  // Added slow speed

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Camera playerCamera;
    private float xRotation = 0f;
    private Vector3 velocity;
    private int jumpCount = 0;
    private int maxJumps = 2;
    private bool isGrounded;
    private bool isSlowed = false;  // Added to track slow state

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleJump();
    }

    private void HandleMovement()
    {
        // Get input axes
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement speed
        float currentSpeed;
        if (isSlowed)
        {
            currentSpeed = slowSpeed;
        }
        else
        {
            currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        }

        // Create movement vector
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply movement
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void SetLowerSpeed(bool slow)
    {
        isSlowed = slow;
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Calculate vertical rotation (looking up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotations
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpCount++;
            Debug.Log("Jumped");
        }
    }
}
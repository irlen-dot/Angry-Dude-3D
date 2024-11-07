using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    // [Header("Ground Check")]
    // [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Lock rotation to prevent the player from falling over
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Check if player is grounded
        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Get horizontal input (A/D or Left/Right arrows)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Get vertical input (W/S or Up/Down arrows)
        verticalInput = Input.GetAxisRaw("Vertical");
        
        // Handle movement
        Move();
    }

    void Move()
    {
        // Calculate movement direction combining horizontal and forward movement
        Vector3 movement = (transform.right * horizontalInput) + (transform.forward * verticalInput);
        
        // Normalize the movement vector to prevent faster diagonal movement
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // Apply movement
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}
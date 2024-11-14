using UnityEngine;

public class ItemThrower : MonoBehaviour
{
    [Header("Throw Settings")]
    [SerializeField] private float throwForce = 20f;
    [SerializeField] private float throwUpwardForce = 2f;
    [SerializeField] private Camera playerCamera;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationForce = 5f;  // Controls how fast the item rotates
    [SerializeField] private bool randomizeRotationAxis = true;  // Whether to randomize rotation axis
    [SerializeField] private Vector3 rotationAxis = Vector3.right;  // Default rotation axis if not randomized

    [Header("Item Settings")]
    [SerializeField] private float itemMass = 1f;
    [SerializeField] private float destroyDelay = 5f;
    [SerializeField] private GameObject debugItemPrefab;
    private GameObject currentItem;
    private Inventory inventory;

    [Header("Debug Settings")]
    [SerializeField] private bool debugMode = true;
    private GameObject originalPrefab;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();
    }

    public void SetItem(GameObject item)
    {
        if (debugMode)
        {
            debugItemPrefab = item;
            originalPrefab = item;
        }
        else
        {
            currentItem = item;
            currentItem.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (debugMode)
            {
                ThrowDebugItem();
            }
            else
            {
                ThrowItem();
            }
        }
    }

    private Vector3 GetRandomRotationAxis()
    {
        return new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }

    void ThrowDebugItem()
    {
        if (playerCamera == null || debugItemPrefab == null) return;

        // Calculate spawn position slightly in front of camera
        Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.forward * 0.5f;

        // Get the exact direction the player is looking using camera's forward
        Vector3 throwDirection = playerCamera.transform.forward.normalized;

        // Create the item and ensure it's not a child of any other object
        GameObject thrownItem = Instantiate(debugItemPrefab, spawnPosition, Quaternion.LookRotation(throwDirection));
        thrownItem.transform.SetParent(null);
        thrownItem.SetActive(true);

        Rigidbody rb = thrownItem.GetComponent<Rigidbody>();
        if (rb == null)
            rb = thrownItem.AddComponent<Rigidbody>();

        // Configure the rigidbody
        rb.mass = itemMass;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = true;

        // Clear any existing physics state
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Calculate throw force using normalized direction
        Vector3 throwForceVector = throwDirection * throwForce;
        if (throwUpwardForce > 0)
        {
            throwForceVector += Vector3.up * throwUpwardForce;
        }

        // Apply force in world space
        rb.AddForce(throwForceVector, ForceMode.Impulse);

        // Apply rotation
        Vector3 rotAxis = randomizeRotationAxis ? GetRandomRotationAxis() : rotationAxis.normalized;
        rb.AddTorque(rotAxis * rotationForce, ForceMode.Impulse);

        if (destroyDelay > 0)
            Destroy(thrownItem, destroyDelay);
    }

    void ThrowItem()
    {
        if (playerCamera == null || currentItem == null) return;

        // Calculate spawn position slightly in front of camera
        Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.forward * 0.5f;

        // Get the exact direction the player is looking using camera's forward
        Vector3 throwDirection = playerCamera.transform.forward.normalized;

        // Position and rotate the item
        currentItem.transform.position = spawnPosition;
        currentItem.transform.rotation = Quaternion.LookRotation(throwDirection);
        currentItem.transform.SetParent(null);
        currentItem.SetActive(true);

        Rigidbody rb = currentItem.GetComponent<Rigidbody>();
        if (rb == null)
            rb = currentItem.AddComponent<Rigidbody>();

        // Configure the rigidbody
        rb.mass = itemMass;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = true;

        // Clear any existing physics state
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Calculate throw force using normalized direction
        Vector3 throwForceVector = throwDirection * throwForce;
        if (throwUpwardForce > 0)
        {
            throwForceVector += Vector3.up * throwUpwardForce;
        }

        // Apply force in world space
        rb.AddForce(throwForceVector, ForceMode.Impulse);

        // Apply rotation
        Vector3 rotAxis = randomizeRotationAxis ? GetRandomRotationAxis() : rotationAxis.normalized;
        rb.AddTorque(rotAxis * rotationForce, ForceMode.Impulse);

        inventory.RemoveItem(currentItem);
        currentItem = null;
    }

    void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.red;
            Vector3 throwDirection = playerCamera.transform.forward.normalized;
            Gizmos.DrawRay(playerCamera.transform.position, throwDirection * 3f);
        }
    }
}
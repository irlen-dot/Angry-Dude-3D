using UnityEngine;

public class ItemThrower : MonoBehaviour
{
    [SerializeField] private float throwForce = 20f;
    [SerializeField] private float upwardForce = 5f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject[] itemPrefabs;

    // Add these variables to help prevent tunneling
    [SerializeField] private float continuousCollisionDetectionThreshold = 5f; // Velocity threshold for CCD
    [SerializeField] private bool useCollisionDetection = true;

    private Inventory inventory;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();

        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ThrowItem();
        }
    }

    void ThrowItem()
    {
        if (playerCamera == null || itemPrefabs.Length == 0) return;

        int itemIndex = inventory.GetCurrentItemIndex();
        if (itemIndex < 0 || itemIndex >= itemPrefabs.Length) return;

        // Spawn slightly in front of the camera
        Vector3 spawnPos = playerCamera.transform.position + playerCamera.transform.forward;
        GameObject thrownItem = Instantiate(itemPrefabs[itemIndex], spawnPos, Quaternion.identity);

        Rigidbody rb = thrownItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Enable gravity for more realistic throwing
            rb.useGravity = true;

            // Enable Continuous Collision Detection
            if (useCollisionDetection)
            {
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }

            // Add a small amount of random rotation for more natural movement
            rb.AddTorque(Random.insideUnitSphere * 0.5f, ForceMode.Impulse);

            // Combine forward force with upward force
            Vector3 throwDirection = playerCamera.transform.forward * throwForce;
            Vector3 upwardDirection = Vector3.up * upwardForce;
            Vector3 combinedForce = throwDirection + upwardDirection;

            rb.AddForce(combinedForce, ForceMode.Impulse);

            // Monitor velocity and adjust collision detection mode
            StartCoroutine(MonitorVelocity(rb));
        }

        inventory.RemoveCurrentItem();
    }

    private System.Collections.IEnumerator MonitorVelocity(Rigidbody rb)
    {
        while (rb != null)
        {
            if (useCollisionDetection)
            {
                // Switch between Continuous and Discrete based on velocity
                if (rb.velocity.magnitude > continuousCollisionDetectionThreshold)
                {
                    rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                }
                else
                {
                    rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
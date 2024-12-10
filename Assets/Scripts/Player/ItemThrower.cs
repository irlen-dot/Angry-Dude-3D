using UnityEngine;

public class ItemThrower : MonoBehaviour
{
    [SerializeField] private float throwForce = 20f;
    [SerializeField] private float upwardForce = 5f;  // Added upward force
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject[] itemPrefabs;

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
            // Combine forward force with upward force
            Vector3 throwDirection = playerCamera.transform.forward * throwForce;
            Vector3 upwardDirection = Vector3.up * upwardForce;
            Vector3 combinedForce = throwDirection + upwardDirection;

            rb.AddForce(combinedForce, ForceMode.Impulse);
        }

        inventory.RemoveCurrentItem();
    }
}
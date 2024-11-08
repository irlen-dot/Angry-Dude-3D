using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is meant to be a component of a player
public class ItemThrower : MonoBehaviour
{
    [Header("Throw settings")]

    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float throwUpwardForce = 2f;
    [SerializeField, Range(0, 90)] private float upwardAngle = 30f;
    [SerializeField] private float heightOffset = 2f;


    private GameObject heldItem; // Reference to currently held item


    void Update()
    {
        if (Input.GetMouseButtonDown(1) && heldItem != null)
        {
            ThrowItem();
        }
    }

    public void SetHeldItem(GameObject item)
    {
        heldItem = item;
    }

    private void ThrowItem()
    {
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = heldItem.AddComponent<Rigidbody>();
        }

        // Enable collider if it exists
        if (heldItem.TryGetComponent<Collider>(out Collider collider))
        {
            collider.enabled = true;
        }

        heldItem.SetActive(true);
        // Reset parent
        heldItem.transform.SetParent(null);

        // Set position with height offset
        Vector3 throwPosition = transform.position + Vector3.up * heightOffset;
        heldItem.transform.position = throwPosition;

        // Calculate throw direction with upward angle using this transform's forward
        Vector3 throwDirection = Quaternion.Euler(-upwardAngle, 0, 0) * transform.forward;

        // Reset rigidbody settings
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Apply forces
        rb.velocity = Vector3.zero; // Reset velocity before throwing
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        rb.AddForce(Vector3.up * throwUpwardForce, ForceMode.Impulse);

        // Optional: Add some rotation
        rb.AddTorque(Random.insideUnitSphere * throwForce, ForceMode.Impulse);

        // Clear reference
        heldItem = null;
    }

    // Optional: Visualize throw direction and position in editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 throwPosition = transform.position + Vector3.up * heightOffset;
        Vector3 throwDirection = Quaternion.Euler(-upwardAngle, 0, 0) * transform.forward;

        // Draw throw position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(throwPosition, 0.2f);

        // Draw throw direction
        Gizmos.color = Color.red;
        Gizmos.DrawRay(throwPosition, throwDirection * 2);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PickUp : MonoBehaviour
{
    private BoxCollider triggerCollider;

    void Start()
    {
        GetTriggerCollider();
    }

    private void GetTriggerCollider()
    {
        // Get all BoxColliders on this object
        BoxCollider[] colliders = GetComponents<BoxCollider>();

        // Find the BoxCollider with isTrigger = true
        foreach (BoxCollider collider in colliders)
        {
            if (collider.isTrigger)
            {
                triggerCollider = collider;
                break;
            }
        }

        // Optional: Check if we found the trigger collider
        if (triggerCollider == null)
        {
            Debug.LogWarning("No trigger BoxCollider found on " + gameObject.name);
        }
    }

    // Called when another collider exits the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger!");
            other.GetComponent<Inventory>();

            // Add your code here for what should happen when player exits
        }
    }
}

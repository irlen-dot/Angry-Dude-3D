using Unity.VisualScripting;
using UnityEngine;

public class NPCRunawaySpot : MonoBehaviour
{
    private bool isAvailable = true;
    public bool IsAvailable { get { return isAvailable; } }

    [SerializeField] private Door door;
    [SerializeField] private float gizmoRadius = 1f; // Add radius parameter for the sphere
    [SerializeField] private Color gizmoColor = Color.yellow; // Add color parameter for the sphere

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Unarmed NPC"))
        {
            Debug.Log("Runaway is triggered.");
            CloseTheRoom();
        }
    }

    private void CloseTheRoom()
    {
        if (isAvailable != true)
            isAvailable = false;
        door.CloseDoor();
        Debug.Log("Unarmed NPC entered the spot.");
    }

    // Draw the Gizmo sphere in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }

    // Draw the Gizmo sphere even when the object is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, gizmoRadius);
    }
}
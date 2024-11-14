using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour  // Made the class public
{
    private BoxCollider boxCollider;
    private MeshRenderer mesh;
    private NavMeshObstacle meshObstacle;

    void Start()
    {
        // Explicitly get both components
        mesh = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        meshObstacle = GetComponent<NavMeshObstacle>();
        // Verify components are found
        if (mesh == null)
            Debug.LogError("MeshRenderer not found on " + gameObject.name);
        if (boxCollider == null)
            Debug.LogError("BoxCollider not found on " + gameObject.name);

        OpenDoor();
    }

    public void CloseDoor()
    {
        ToggleDoor(true);
    }

    public void OpenDoor()
    {
        ToggleDoor(false);
    }

    private void ToggleDoor(bool isClosed)
    {
        Debug.Log($"Door state before change - Collider enabled: {boxCollider.enabled}, Mesh enabled: {mesh.enabled}");

        // Set both states
        meshObstacle.enabled = isClosed;
        boxCollider.enabled = isClosed;
        mesh.enabled = isClosed;

        Debug.Log($"Door state after change - Collider enabled: {boxCollider.enabled}, Mesh enabled: {mesh.enabled}");
    }
}
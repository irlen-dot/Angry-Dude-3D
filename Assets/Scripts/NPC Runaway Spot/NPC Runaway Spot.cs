using Unity.VisualScripting;
using UnityEngine;

public class NPCRunawaySpot : MonoBehaviour
{
    private bool isAvailable = true;
    public bool IsAvailable { get { return isAvailable; } }

    [SerializeField] private Door door;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Unarmed NPC"))
        {
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

}

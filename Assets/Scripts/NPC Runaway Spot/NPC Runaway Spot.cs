using Unity.VisualScripting;
using UnityEngine;

public class NPCRunawaySpot : MonoBehaviour
{
    private bool isAvailable = true;
    public bool IsAvailable { get { return isAvailable; } }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Unarmed NPC"))
        {
            Disable();
        }
    }


    private void Disable()
    {
        if (isAvailable != true)
            isAvailable = false;
    }

}

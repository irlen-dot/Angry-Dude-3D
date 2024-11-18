using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    private BoxCollider boxCollider;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Armed NPC"))
        {
            // Debug.Log("Colzdfihvbzhdifbvioxdbtoid");
            ProcessHit(other.gameObject.GetComponent<ArmedNPC>());
        }
    }

    void ProcessHit(ArmedNPC npc)
    {
        AttackType currentAttack = npc.CurrentAttack;
        if (currentAttack == AttackType.Hit)
        {
            Debug.Log("You got hit");
        }
    }
}

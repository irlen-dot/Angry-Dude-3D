using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ThrowRange : MonoBehaviour
{
    private ItemThrower itemThrower;
    private int NPCsInside = 0;
    // private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        itemThrower = GetComponent<ItemThrower>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Armed NPC"))
        {
            // if (NPCsInside < 1)/rentTarget = other.gameObject;
            NPCsInside++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Armed NPC"))
        {
            // if (NPCsInside == 0)
            //     itemThrower.CurrentTarget = null;
            NPCsInside--;
        }
    }
}

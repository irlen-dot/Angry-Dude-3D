using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCType
{
    Armed = 0,
    Unarmed = 1,
}

public class SpawnPoint : MonoBehaviour
{
    private GameObject armedNPC;
    private GameObject unarmedNPC;

    void Awake()
    {
        armedNPC = transform.Find("Armed NPC").gameObject;
        unarmedNPC = transform.Find("Unarmed NPC").gameObject;
        armedNPC.SetActive(false);
        unarmedNPC.SetActive(false);
        armedNPC.transform.position = transform.position;
        unarmedNPC.transform.position = transform.position;
    }

    public void SpawnNPC(NPCType type)
    {
        if (type == NPCType.Armed)
        {
            armedNPC.SetActive(true);
        }
        if (type == NPCType.Unarmed)
        {
            unarmedNPC.SetActive(true);
        }
    }
}

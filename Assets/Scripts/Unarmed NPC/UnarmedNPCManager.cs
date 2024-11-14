using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnarmedNPCManager : MonoBehaviour
{
    NPCRunawaySpot[] nPCRunawaySpots;
    UnarmedNPC[] unarmedNPCs;

    // Start is called before the first frame update
    void Start()
    {
        unarmedNPCs = GetComponentsInChildren<UnarmedNPC>();
        nPCRunawaySpots = FindObjectsOfType<NPCRunawaySpot>();
        LinkNPCToPoints();
    }

    void LinkNPCToPoints()
    {
        Debug.Log("Starting to link npcs to point...");
        Debug.Log(unarmedNPCs.Length);
        for (int i = 0; i < nPCRunawaySpots.Length; i++)
        {
            Debug.Log($"Started Linking spot number {i}...");
            Debug.Log(unarmedNPCs[i]);
            unarmedNPCs[i].SetGoal(nPCRunawaySpots[i].transform.position);
            Debug.Log($"Linked spot number {i}.");
        }
        Debug.Log("Linked points to npcs.");
    }
}

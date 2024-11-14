using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    [Tooltip("You set the spawn points that share the borders with the current room")]
    private SpawnPoint[] closestSpawnPoints;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ActivateSpawnPoints();
        }
    }

    // Basically, just tells to spawn the guys around you
    private void ActivateSpawnPoints()
    {
        foreach (SpawnPoint spawnPoint in closestSpawnPoints)
        {
            spawnPoint.SpawnNPC(NPCType.Armed);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnarmedNPC : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    // I used here on Awake, becuase the SetGoal was triggered before the navMeshAgent got it's component
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetGoal(Vector3 destination)
    {
        Debug.Log("Starting to set the goal...");
        Debug.Log($"Destination: {navMeshAgent}");
        navMeshAgent.destination = destination;
        Debug.Log("Goal is set...");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Weapon"))
        {
            Destroy(gameObject);
        }
    }


}

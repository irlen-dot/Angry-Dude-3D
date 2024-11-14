using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ArmedNPC : MonoBehaviour
{


    private NavMeshAgent navMeshAgent;
    private Mover player;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<Mover>();
    }

    void OnEnable()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    void FixedUpdate()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }
}

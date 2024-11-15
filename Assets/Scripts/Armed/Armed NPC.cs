using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ArmedNPC : MonoBehaviour
{


    private NavMeshAgent navMeshAgent;
    private Mover player;

    private AttackTypeManager attackTypeManager;

    private AttackSequence currentAttackSequence;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<Mover>();
        attackTypeManager = FindObjectOfType<AttackTypeManager>();
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

using Unity.VisualScripting;
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

    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ArmedNPC : MonoBehaviour
{
    // ============== PROPERTIES START
    private NavMeshAgent navMeshAgent;

    private Mover player;

    [SerializeField] private List<AttackSequence> currentAttackSequence = new List<AttackSequence>();

    [SerializeField]
    private float dashForce = 10f;

    public List<AttackSequence> CurrentAttackSequence { set { currentAttackSequence = value; } }

    public AttackType CurrentAttack { get { return currentAttack; } }

    private int currentSequenceIndex = 0;

    private int currentMoveIndex = 0;

    private AttackType currentAttack;

    private bool isDashed;

    private float originalSpeed;
    private Rigidbody rb;

    // ============== PROPERTIES END


    public void LogTheSequence()
    {
        Debug.Log($"The current Attack sequence is {currentAttackSequence[0].sequence[1]}");
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        navMeshAgent = GetComponent<NavMeshAgent>();
        originalSpeed = navMeshAgent.speed;
        player = FindFirstObjectByType<Mover>();
    }

    void OnEnable()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    void FixedUpdate()
    {
        navMeshAgent.SetDestination(player.transform.position);
        if (currentAttackSequence.Count > 0)
        {
            MakeMove();
        }
    }

    public void ProcessDamage()
    {
        Debug.Log("NPC got damage.");
    }

    public void ProcessShotgunDamage()
    {
        Debug.Log("NPC got shotgun damage.");
    }


    private void MakeMove()
    {
        List<AttackType> currentSequence = currentAttackSequence[currentSequenceIndex].sequence;
        AttackType attackType = currentSequence[currentMoveIndex];
        currentAttack = attackType;

        Debug.Log($"The current move is: {attackType}");

        switch (attackType)
        {
            case AttackType.Hit:
                ProcessHit();
                break;
            case AttackType.Pass:
                ProcessPass();
                break;
            case AttackType.Preparation:
                ProcessPreparation();
                break;
            default:
                break;
        }

        if (currentMoveIndex >= (currentSequence.Count - 1))
        {
            currentSequenceIndex++;
            currentMoveIndex = 0;
        }
        else
        {
            currentMoveIndex++;
        }
    }

    private void ResetDash()
    {
        if (isDashed)
        {
            rb.velocity = Vector3.zero; // Stop the dash
            navMeshAgent.enabled = true; // Re-enable NavMeshAgent
            isDashed = false;
        }
    }

    private void ProcessPreparation()
    {
        ResetDash();
    }

    private void ProcessPass()
    {
        ResetDash();
    }

    private void StartDash()
    {

        isDashed = true;
        navMeshAgent.enabled = false; // Disable NavMeshAgent during dash
        Vector3 dashDirection = (player.transform.position - transform.position).normalized;
        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);

        // yield return new WaitForSeconds(Time.deltaTime);

    }

    private void ProcessHit()
    {
        StartDash();
        Debug.Log("Dash is turned on.");
    }
}

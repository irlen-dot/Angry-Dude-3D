using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArmedNPC : MonoBehaviour
{
    // Previous properties remain the same
    [SerializeField] private List<AttackSequence> currentAttackSequence = new List<AttackSequence>();
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float attackRadius = 1.5f; // Radius to check for player collision during attack

    private Mover player;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private Health playerHealth;

    private bool isDashed;
    private float originalSpeed;
    private int currentSequenceIndex = 0;
    private int currentMoveIndex = 0;
    private AttackType currentAttack;
    private bool isAttacking = false;

    public List<AttackSequence> CurrentAttackSequence { set { currentAttackSequence = value; } }
    public AttackType CurrentAttack { get { return currentAttack; } }

    private bool canHit = false;

    public bool CanHit { set { canHit = value; } }

    void Awake()
    {
        playerHealth = FindFirstObjectByType<Health>();
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

        // Check for player hit during attack
        if (isAttacking)
        {
            CheckPlayerHit();
        }
    }

    private void CheckPlayerHit()
    {
        // Check if player is within attack radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
        foreach (var hitCollider in hitColliders)
        {
            Health playerHealth = hitCollider.GetComponent<Health>();
            if (playerHealth != null)
            {
                // We found the player, trigger the hit
                isAttacking = false; // Prevent multiple hits from same attack
                Debug.Log("Player in attack range!");
                return;
            }
        }
    }

    public void ProcessShotgunDamage()
    {
        // TODO add death animation and shit
        Debug.Log("Takes a shotgun shot");
        Destroy(gameObject);
    }

    public void ProcessDamage()
    {
        // TODO add a npc death animation and return it to the object pool
        Debug.Log("NPC is dead");
        Destroy(gameObject);
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
            rb.velocity = Vector3.zero;
            navMeshAgent.enabled = true;
            isDashed = false;
            isAttacking = false;
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
        isAttacking = true;
        navMeshAgent.enabled = false;
        Vector3 dashDirection = (player.transform.position - transform.position).normalized;
        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
    }

    private void ProcessHit()
    {
        StartDash();
        Debug.Log($"Armed NPC -- Can hit is: {canHit}");
        if (canHit)
        {
            playerHealth.TakeDamage();
        }
        Debug.Log("Armed NPC -- Dash is turned on.");
    }

    // Optional: Visualize the attack radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
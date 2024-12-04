using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField][Range(1, 3)] private int healthLeft;
    [SerializeField] private float regenerationDelay = 10f;  // Time in seconds before health regenerates

    private BoxCollider boxCollider;
    private Coroutine regenerationCoroutine;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Armed NPC"))
        {
            ProcessHit(other.gameObject.GetComponent<ArmedNPC>());
        }
    }

    void ProcessHit(ArmedNPC npc)
    {
        AttackType currentAttack = npc.CurrentAttack;
        if (currentAttack == AttackType.Hit)
        {
            Debug.Log("You got hit");

            // Stop any existing regeneration coroutine
            if (regenerationCoroutine != null)
            {
                StopCoroutine(regenerationCoroutine);
            }

            // Start a new regeneration coroutine
            regenerationCoroutine = StartCoroutine(RegenerateHealthAfterDelay());
        }
    }

    private IEnumerator RegenerateHealthAfterDelay()
    {
        yield return new WaitForSeconds(regenerationDelay);

        if (healthLeft < 3)  // Assuming 3 is the maximum health
        {
            healthLeft++;
            Debug.Log($"Health regenerated. Current health: {healthLeft}");
        }
    }
}
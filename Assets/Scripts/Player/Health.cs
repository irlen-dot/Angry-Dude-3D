using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class Health : MonoBehaviour
{
    [SerializeField][Range(1, 3)] private int healthLeft;
    [SerializeField] private float regenerationDelay = 10f;  // Time in seconds before health regenerates

    private BoxCollider boxCollider;
    private Coroutine regenerationCoroutine;

    private InGameInterface inGameInterface;

    void Awake()
    {
        inGameInterface = FindFirstObjectByType<InGameInterface>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Armed NPC"))
        {
            Debug.Log("Health class -- the Armed npc is in");
            AllowToHit(other.gameObject);
        }
    }

    private void AllowToHit(GameObject armedNPC)
    {
        Debug.Log("Health class -- You've allowed to hit you");
        armedNPC.GetComponent<ArmedNPC>().CanHit = true;

    }

    public void TakeDamage()
    {

        Debug.Log("Health -- You got hit");



        // Stop any existing regeneration coroutine
        if (regenerationCoroutine != null)
        {
            StopCoroutine(regenerationCoroutine);
            DecreaseHealth();
        }

        // Start a new regeneration coroutine
        regenerationCoroutine = StartCoroutine(RegenerateHealthAfterDelay());
    }

    private void DecreaseHealth()
    {
        healthLeft--;
        if (healthLeft <= 0)
        {
            Debug.Log("You lost.");
        }
    }

    // private void 

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
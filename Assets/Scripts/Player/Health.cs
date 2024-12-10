using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    private int healthLeft;
    [SerializeField] private float regenerationDelay = 10f;  // Time in seconds before health regenerates
    [SerializeField][Range(1, 3)] private int maxHealth = 3;  // Added to make max health configurable

    private Coroutine regenerationCoroutine;
    private InGameInterface inGameInterface;

    void Awake()
    {
        healthLeft = maxHealth;
        inGameInterface = FindFirstObjectByType<InGameInterface>();
    }

    void Start()
    {
        inGameInterface.SetHealth(healthLeft);
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
        inGameInterface.SetHealth(healthLeft);
        if (healthLeft <= 0)
        {
            Debug.Log("You lost.");
        }
    }

    public void IncreaseHealth(int amount = 1)
    {
        // Calculate how much health can actually be added without exceeding max
        int actualIncrease = Mathf.Min(amount, maxHealth - healthLeft);

        if (actualIncrease > 0)
        {
            healthLeft += actualIncrease;
            inGameInterface.SetHealth(healthLeft);
            Debug.Log($"Health increased by {actualIncrease}. Current health: {healthLeft}");
        }
        else
        {
            Debug.Log("Health is already at maximum!");
        }
    }

    private IEnumerator RegenerateHealthAfterDelay()
    {
        yield return new WaitForSeconds(regenerationDelay);

        if (healthLeft < maxHealth)
        {
            IncreaseHealth();  // Use the new IncreaseHealth method
        }
    }
}
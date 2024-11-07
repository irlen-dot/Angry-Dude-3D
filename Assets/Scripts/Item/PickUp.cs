using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private ItemsEnum itemType;

    public ItemsEnum ItemType
    {
        get { return itemType; }
    }

    private bool canPickup = false;

    private void Update()
    {
        // Check for input in Update instead of OnTriggerStay
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"Picking up {itemType}...");
            Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            inventory.SetItem(gameObject);
            canPickup = false; // Reset the flag
            gameObject.SetActive(false);
            Debug.Log($"Picked up the {itemType}.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You entered into an item pickup zone.");
            canPickup = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You exited into an item pickup zone.");
            canPickup = false;
        }
    }

    // OnTriggerStay is no longer needed
}
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private ItemsEnum itemType;
    [SerializeField] private string itemName = "Some Item";

    public ItemsEnum ItemType => itemType;
    public string ItemName => itemName;

    private bool canPickup = false;
    private Inventory playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            playerInventory = other.GetComponent<Inventory>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            playerInventory = null;
        }
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }
    }

    private void PickUpItem()
    {
        if (playerInventory != null)
        {
            playerInventory.PickupItem(itemType);
            Destroy(gameObject);  // Remove the world item once picked up
        }
    }
}
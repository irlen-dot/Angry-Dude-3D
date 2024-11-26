using UnityEngine;

public class PickUp : MonoBehaviour
{
    // [SerializeField]
    // private ItemsEnum itemType;

    [SerializeField]
    private Item itemInfo;

    private ItemThrower itemThrower;

    public Item ItemInfo
    {
        get { return itemInfo; }
    }

    private bool canPickup = false;
    private KeyCode pickIpKey = KeyCode.E;

    private void Awake()
    {
        pickIpKey = KeyCode.E;
        itemThrower = FindFirstObjectByType<ItemThrower>();
    }

    private void Update()
    {
        // Check for input in Update instead of OnTriggerStay
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }
    }

    private void PickUpItem()
    {
        Debug.Log($"Picking up {itemType}...");
        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        inventory.SetItem(itemType);
        canPickup = false;
        // TODO return
        itemThrower.SetItem(gameObject, itemType);
        // I've linked the positions, assuming that the item thrower is linked to the player
        gameObject.transform.position = itemThrower.transform.position;
        gameObject.SetActive(false);
        Debug.Log($"Picked up the {itemType}.");
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
}
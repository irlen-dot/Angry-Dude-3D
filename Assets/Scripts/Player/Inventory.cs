using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefabs;  // Array matching ItemsEnum order
    private int currentItemIndex = -1;
    private GameObject currentVisualItem;

    private void Start()
    {
        InitItems();
    }

    private void InitItems()
    {
        GameObject items = transform.Find("Items").gameObject;
        // Deactivate all visual items at start
        foreach (Transform item in items.transform)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void PickupItem(ItemsEnum itemType)
    {
        // Deactivate current visual item if exists
        if (currentVisualItem != null)
        {
            currentVisualItem.SetActive(false);
        }

        // Set new item
        currentItemIndex = (int)itemType;

        // Find and activate the visual representation
        GameObject items = transform.Find("Items").gameObject;
        foreach (Transform item in items.transform)
        {
            if (item.GetComponent<PickUp>().ItemType == itemType)
            {
                currentVisualItem = item.gameObject;
                currentVisualItem.SetActive(true);
                break;
            }
        }
    }

    public int GetCurrentItemIndex()
    {
        return currentItemIndex;
    }

    public void RemoveCurrentItem()
    {
        if (currentVisualItem != null)
        {
            currentVisualItem.SetActive(false);
        }
        currentVisualItem = null;
        currentItemIndex = -1;
    }
}
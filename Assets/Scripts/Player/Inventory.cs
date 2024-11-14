using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    GameObject currentItem;
    Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitItems();
    }

    private void InitItems()
    {
        Debug.Log("Items initing in the inventory...");
        GameObject items = transform.Find("Items").gameObject;
        foreach (Transform item in items.transform)
        {
            this.items.Add(item.name, item.gameObject);
            this.items[item.name].SetActive(false);
            Debug.Log($"{item.name} is inited.");
        }
    }

    public void SetItem(GameObject item)
    {
        Debug.Log($"Setting {item.name} in the inventory...");
        ToggleItem(item, true);
        Debug.Log($"Setted {item.name} in the inventory.");
    }

    public void RemoveItem(GameObject item)
    {
        Debug.Log($"Removing {item.name} from the inventory...");
        ToggleItem(item, false);
        Debug.Log($"Removed {item.name} from the inventory.");
    }

    private void ToggleItem(GameObject item, bool isActive)
    {
        string itemType = item.GetComponent<PickUp>().ItemType.ToString();
        items[itemType].SetActive(isActive);
        if (isActive) { currentItem = item; }
        else { currentItem = null; }

    }
}

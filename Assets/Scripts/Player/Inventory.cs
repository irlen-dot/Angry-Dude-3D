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
        GameObject items = transform.Find("PlayerCameraRoot").transform.Find("Items").gameObject;
        foreach (Transform item in items.transform)
        {

            this.items.Add(item.name, item.gameObject);
            this.items[item.name].SetActive(false);
            Debug.Log($"{item.name} is inited.");
        }
    }


    public void SetItem(GameObject item)
    {

        Debug.Log($"Settig {item.name} in the inventory...");
        string itemType = item.GetComponent<PickUp>().ItemType.ToString();
        items[itemType].SetActive(true);
        currentItem = item;
        Debug.Log($"{itemType} is setted");
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private GameObject currentItem;
    private ItemsEnum? currentItemType;
    private bool currentIsHeavy;
    public bool IsHeavy { get { return currentIsHeavy; } }
    private Dictionary<ItemsEnum, GameObject> items = new Dictionary<ItemsEnum, GameObject>();
    private Hit hit;
    private Shoot shoot;
    private Mover mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<Mover>();
        hit = transform.Find("Hit Range").GetComponent<Hit>();
        // TODO removSe line
        hit.enabled = false;
        shoot = GetComponent<Shoot>();
        InitItems();
    }

    private void InitItems()
    {
        Debug.Log("Items initing in the inventory...");
        GameObject items = transform.Find("Items").gameObject;
        foreach (Transform item in items.transform)
        {
            ItemsEnum itemType = item.GetComponent<PickUp>().ItemType;
            this.items.Add(itemType, item.gameObject);
            this.items[itemType].SetActive(false);
            if (itemType == ItemsEnum.ShotGun)
            {
                this.items[itemType].SetActive(true);
            }
            Debug.Log($"{item.name} is inited.");
        }
    }

    public void SetItem(ItemsEnum item)
    {
        ToggleItem(item, true);
    }

    public void RemoveItem(ItemsEnum item)
    {
        ToggleItem(item, false);
    }

    private void ToggleItem(ItemsEnum itemType, bool isActive)
    {
        if (currentItem != null)
            currentItem.SetActive(false);

        GameObject item = items[itemType];
        item.SetActive(isActive);

        if (isActive)
        {

            ItemInfo itemInfo = currentItem.gameObject.GetComponent<Item>().ItemInfo;
            currentItem = item;
            currentItemType = itemInfo.ItemType;
            currentIsHeavy = itemInfo.IsHeavy;
            // mover.SetLowerSpeed()
        }
        else
        {
            currentItem = null;
            currentItemType = null;
            currentIsHeavy = false;
        }
    }
}

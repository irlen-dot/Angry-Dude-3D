using System;
using UnityEngine;


public class ItemManager : MonoBehaviour
{
    [SerializeField]
    ItemInfo[] items;
    // Start is called before the first frame update

    public ItemInfo GetItem(string name)
    {
        ItemInfo item = Array.Find<ItemInfo>(items, item => item.Name == name);
        return item;
    }
}

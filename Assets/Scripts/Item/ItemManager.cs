using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface for items
public interface IItem
{
    string Name { get; set; }
    GameObject ItemObject { get; set; }
}

// Concrete implementation of IItem
[System.Serializable]
public class Item : IItem
{
    public string Name { get; set; }
    public GameObject ItemObject { get; set; }
}

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    Item[] items;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Item GetItem(String name)
    {
        Item item = Array.Find<Item>(items, item => item.Name == name);
        return item;
    }
}

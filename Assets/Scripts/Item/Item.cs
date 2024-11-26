using UnityEngine;

// Making the class serializable
[System.Serializable]
public class ItemInfo
{
    [SerializeField] private bool isHeavy;
    [SerializeField] private string itemName;
    [SerializeField] private ItemsEnum itemType;

    // Public properties that use the private fields
    public bool IsHeavy { get => isHeavy; set => isHeavy = value; }
    public string Name { get => itemName; set => itemName = value; }
    public ItemsEnum ItemType { get => itemType; set => itemType = value; }
}

public class Item : MonoBehaviour
{
    // Make sure to mark this as SerializeField to show in inspector
    [SerializeField]
    private ItemInfo itemInfo = new ItemInfo();

    public ItemInfo ItemInfo { get => itemInfo; }

    // Optional: Add this if you want to modify values at runtime through code
    private void OnValidate()
    {
        // Ensure itemInfo is never null in the inspector
        if (itemInfo == null)
            itemInfo = new ItemInfo();
    }
}
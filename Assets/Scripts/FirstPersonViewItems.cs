using UnityEngine;
using System.Collections.Generic;

public class FirstPersonViewItems : MonoBehaviour
{
    List<GameObject> items = new List<GameObject>();
    public int weaponLayer = 6; // Make sure this matches the layer in CameraSetup

    // void Start()
    // {
    //     foreach (Transform child in transform)
    //     {
    //         items.Add(child.gameObject);
    //         SetLayerRecursively(child.gameObject, weaponLayer);
    //     }
    // }

    // void SetLayerRecursively(GameObject obj, int newLayer)
    // {
    //     if (obj == null) return;
    //     obj.layer = newLayer;

    //     foreach (Transform child in obj.transform)
    //     {
    //         SetLayerRecursively(child.gameObject, newLayer);
    //     }
    // }
}
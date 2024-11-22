using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    public int weaponLayer = 6; // Make sure this matches the layer number you're using

    void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.nearClipPlane = 0.01f;

            // Remove the weapon layer from culling
            int everythingExceptWeapon = ~(1 << weaponLayer);
            mainCamera.cullingMask = everythingExceptWeapon;

            Debug.Log($"Camera setup complete. Weapon layer {weaponLayer} excluded from culling");
        }
    }
}
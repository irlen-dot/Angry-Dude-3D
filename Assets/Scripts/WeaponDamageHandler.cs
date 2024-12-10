using UnityEngine;

public class WeaponDamageHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("The NPC was hit.");
            other.gameObject.GetComponent<ItemVFX>().StartVFX();
            Destroy(gameObject);
        }
    }
}
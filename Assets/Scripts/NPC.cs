using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public void ProcessDamage()
    {
        Destroy(gameObject);
        Debug.Log("NPC got damage.");
    }
}

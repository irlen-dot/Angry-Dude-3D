using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CombatManager : MonoBehaviour
{
    private List<ArmedNPC> armedNPCs;
    private AttackTypeManager attackManager;

    void Start()
    {
        attackManager = GetComponent<AttackTypeManager>();
    }

    // void

    public void AddEnabledNPC(ArmedNPC npc)
    {
        armedNPCs.Add(npc);
    }
}

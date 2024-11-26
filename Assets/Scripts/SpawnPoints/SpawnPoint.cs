using UnityEngine;

public enum NPCType
{
    Armed = 0,
    Unarmed = 1,
}

public class SpawnPoint : MonoBehaviour
{
    private GameObject armedNPC;
    private GameObject unarmedNPC;

    private CombatManager combatManager;

    void Awake()
    {
        InitNPCs();
        combatManager = FindFirstObjectByType<CombatManager>();
    }

    private void InitNPCs()
    {
        armedNPC = transform.Find("Armed NPC").gameObject;
        unarmedNPC = transform.Find("Unarmed NPC").gameObject;
        armedNPC.SetActive(false);
        unarmedNPC.SetActive(false);
        armedNPC.transform.position = transform.position;
        unarmedNPC.transform.position = transform.position;
    }

    public void SpawnNPC(NPCType type)
    {
        if (type == NPCType.Armed)
        {
            SpawnArmed();
        }
        if (type == NPCType.Unarmed)
        {
            unarmedNPC.SetActive(true);
        }
    }

    private void SpawnArmed()
    {
        armedNPC.SetActive(true);
        combatManager.AddEnabledNPC(armedNPC.GetComponent<ArmedNPC>());
    }
}

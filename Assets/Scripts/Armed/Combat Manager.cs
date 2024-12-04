using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private SongManager songManager;
    private List<ArmedNPC> armedNPCs = new List<ArmedNPC>();
    private AttackTypeManager attackManager;

    private List<List<AttackSequence>> currentAttackSequences = new List<List<AttackSequence>>();

    void Start()
    {
        InitializeManagers();
    }

    private void InitializeManagers()
    {
        attackManager = GetComponent<AttackTypeManager>();
        if (attackManager == null)
        {
            Debug.LogError("AttackTypeManager not found!");
            return;
        }

        songManager = FindObjectOfType<SongManager>();
        if (songManager == null)
        {
            Debug.LogError("No SongManager found in the scene!");
            return;
        }

        // Subscribe to events
        songManager.SongPartChangedEvent += OnSongPartChanged;
        songManager.OnManagerInitialized += OnSongManagerInitialized;

        if (songManager.IsInitialized)
        {
            // If SongManager is already initialized, get current state
            InitializeWithCurrentSongPart();
        }

    }

    private void InitializeWithCurrentSongPart()
    {
        PartOfTheSong currentPart = songManager.GetCurrentPart();
        try
        {
            if (attackManager.CachedAttackSequence.ContainsKey(currentPart))
            {
                currentAttackSequences = attackManager.CachedAttackSequence[currentPart];
                UpdateAllNPCSequences();

            }
            else
            {
                Debug.LogWarning($"No attack sequences found for part: {currentPart}");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error initializing combat manager: {e.Message}");
        }
    }

    private void OnSongManagerInitialized()
    {
        InitializeWithCurrentSongPart();
    }

    void OnDestroy()
    {
        if (songManager != null)
        {
            songManager.SongPartChangedEvent -= OnSongPartChanged;
            songManager.OnManagerInitialized -= OnSongManagerInitialized;
        }
    }

    private void OnSongPartChanged(PartOfTheSong newPart)
    {

        if (!attackManager.CachedAttackSequence.ContainsKey(newPart))
        {
            Debug.LogWarning($"No attack sequences found for new part: {newPart}");
            return;
        }

        currentAttackSequences = attackManager.CachedAttackSequence[newPart];
        UpdateAllNPCSequences();
    }

    private void UpdateAllNPCSequences()
    {
        for (int i = 0; i < armedNPCs.Count; i++)
        {
            if (i < currentAttackSequences.Count)
            {
                armedNPCs[i].CurrentAttackSequence = currentAttackSequences[i];
                Debug.Log($"currentAttackSequences hahahah {currentAttackSequences.Count}");
            }
            else
            {
                Debug.LogWarning($"Not enough attack sequences for NPC {i}");
            }
        }
    }

    public void AddEnabledNPC(ArmedNPC npc)
    {

        if (currentAttackSequences.Count <= armedNPCs.Count)
        {
            Debug.LogError("Not enough attack sequences for new NPC!");
            return;
        }

        armedNPCs.Add(npc);
        List<AttackSequence> sequence = currentAttackSequences[armedNPCs.Count - 1];
        npc.CurrentAttackSequence = sequence;


        PartOfTheSong currentPart = songManager.GetCurrentPart();
        AdjustNPCForSongPart(npc, currentPart);
    }

    private void AdjustNPCForSongPart(ArmedNPC npc, PartOfTheSong songPart)
    {
        // Your adjustment logic here
    }

    // Helper method to check the state of the manager
    public void LogCurrentState()
    {

        for (int i = 0; i < armedNPCs.Count; i++)
        {
            // Debug.Log($"NPC {i} sequence count: {armedNPCs[i].CurrentAttackSequence?.Count ?? 0}");
        }
    }
}
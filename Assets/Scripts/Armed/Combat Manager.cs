using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] bool isDebug = true;
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

        if (isDebug)
        {
            Debug.Log("CombatManager initialized");
        }
    }

    private void InitializeWithCurrentSongPart()
    {
        PartOfTheSong currentPart = songManager.GetCurrentPart();
        if (isDebug)
        {
            Debug.Log($"Initializing CombatManager with song part: {currentPart}");
        }
        try
        {
            if (attackManager.CachedAttackSequence.ContainsKey(currentPart))
            {
                currentAttackSequences = attackManager.CachedAttackSequence[currentPart];
                UpdateAllNPCSequences();

                if (isDebug)
                {
                    Debug.Log($"Initialized with {currentAttackSequences.Count} attack sequences");
                }
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
        if (isDebug)
        {
            Debug.Log("SongManager initialized, setting up combat state");
        }
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
        if (isDebug)
        {
            Debug.Log($"Song part changed to: {newPart}");
        }

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
                if (isDebug)
                {
                    Debug.Log($"Updated NPC {i} with new sequence");
                    armedNPCs[i].LogTheSequence();
                }
            }
            else
            {
                Debug.LogWarning($"Not enough attack sequences for NPC {i}");
            }
        }
    }

    public void AddEnabledNPC(ArmedNPC npc)
    {
        if (isDebug)
        {
            Debug.Log($"Adding new NPC. Current NPCs: {armedNPCs.Count}, Available sequences: {currentAttackSequences.Count}");
        }

        if (currentAttackSequences.Count <= armedNPCs.Count)
        {
            Debug.LogError("Not enough attack sequences for new NPC!");
            return;
        }

        armedNPCs.Add(npc);
        List<AttackSequence> sequence = currentAttackSequences[armedNPCs.Count - 1];
        npc.CurrentAttackSequence = sequence;

        if (isDebug)
        {
            Debug.Log($"NPC added with {sequence.Count} attacks");
            npc.LogTheSequence();
        }

        PartOfTheSong currentPart = songManager.GetCurrentPart();
        AdjustNPCForSongPart(npc, currentPart);
    }

    private void AdjustNPCForSongPart(ArmedNPC npc, PartOfTheSong songPart)
    {
        if (isDebug)
        {
            Debug.Log($"Adjusting NPC for part: {songPart}");
        }
        // Your adjustment logic here
    }

    // Helper method to check the state of the manager
    public void LogCurrentState()
    {
        Debug.Log($"Combat Manager State:");
        Debug.Log($"NPCs: {armedNPCs.Count}");
        Debug.Log($"Current Attack Sequences: {currentAttackSequences?.Count ?? 0}");
        Debug.Log($"Current Song Part: {songManager?.GetCurrentPart()}");

        for (int i = 0; i < armedNPCs.Count; i++)
        {
            // Debug.Log($"NPC {i} sequence count: {armedNPCs[i].CurrentAttackSequence?.Count ?? 0}");
        }
    }
}
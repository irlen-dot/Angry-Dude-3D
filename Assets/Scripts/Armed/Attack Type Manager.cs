using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AttackTypeManager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Required to set the attacks")]
    private List<AttackSequence> attackSequence;

    private List<AttackSequence> AttackSequences { get { return attackSequence; } }

    // Dictionary storing unique attack sequence combinations for each part
    private Dictionary<PartOfTheSong, List<List<AttackSequence>>> cachedAttackSequence =
        new Dictionary<PartOfTheSong, List<List<AttackSequence>>>();

    public Dictionary<PartOfTheSong, List<List<AttackSequence>>> CachedAttackSequence { get { return cachedAttackSequence; } }

    void Awake()
    {
        InitAttackSequence();
    }

    private void RotateListLastToFirst<T>(List<T> list)
    {
        if (list == null || list.Count <= 1) return;

        T lastItem = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        list.Insert(0, lastItem);
    }

    private void InitAttackSequence()
    {
        foreach (PartOfTheSong partOfTheSong in Enum.GetValues(typeof(PartOfTheSong)))
        {
            // Get all sequences for this part of song
            List<AttackSequence> matchingSequences = attackSequence
                .Where(sequence => sequence.partOfTheSong == partOfTheSong)
                .ToList();
            Debug.Log($"The sequenced the matched were: {attackSequence.Count}");

            if (matchingSequences.Count == 0)
            {
                Debug.LogWarning($"No attack sequences found for part: {partOfTheSong}");
                continue;
            }

            // Create multiple unique shuffled versions
            List<List<AttackSequence>> uniqueSequences = new List<List<AttackSequence>>();

            // Debug.Log("===============");
            // Calculate how many unique permutations we can make
            // We'll create as many as possible without repeating
            int numberOfUniqueSequences = CalculateFactorial(matchingSequences.Count);
            int maxSequences = Mathf.Min(numberOfUniqueSequences, 10); // Limit to 10 or less


            while (uniqueSequences.Count < maxSequences)
            {
                // Create a new shuffled sequence
                List<AttackSequence> shuffledSequence = matchingSequences
                    .OrderBy(x => UnityEngine.Random.value)
                    .ToList();

                // Check if this exact sequence already exists
                if (!HasMatchingSequence(uniqueSequences, shuffledSequence))
                {
                    uniqueSequences.Add(shuffledSequence);
                }
            }

            cachedAttackSequence[partOfTheSong] = uniqueSequences;
            Debug.Log($"Was set in the cachedAttackSequence.");
        }
    }

    // Helper method to check if a sequence combination already exists
    private bool HasMatchingSequence(List<List<AttackSequence>> existingSequences, List<AttackSequence> newSequence)
    {
        foreach (var existing in existingSequences)
        {
            if (existing.Count != newSequence.Count) continue;

            bool isMatch = true;
            for (int i = 0; i < existing.Count; i++)
            {
                if (existing[i] != newSequence[i])
                {
                    isMatch = false;
                    break;
                }
            }
            if (isMatch) return true;
        }
        return false;
    }

    // Helper method to calculate factorial (for permutation count)
    private int CalculateFactorial(int n)
    {
        if (n <= 1) return 1;
        int result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
            if (result > 1000) return 1000; // Prevent overflow, cap at 1000
        }
        return result;
    }

    public List<AttackSequence> GetNextSequence(PartOfTheSong partOfTheSong)
    {
        if (!cachedAttackSequence.ContainsKey(partOfTheSong) ||
            cachedAttackSequence[partOfTheSong].Count == 0)
        {
            Debug.LogWarning($"No cached sequences for part: {partOfTheSong}");
            return new List<AttackSequence>();
        }

        var sequences = cachedAttackSequence[partOfTheSong];

        // Rotate the list of sequences
        RotateListLastToFirst(sequences);

        // Return the first sequence (which was previously the last)
        return sequences[0];
    }

    // Debug method to check unique combinations
    public void DebugPrintCachedSequences()
    {
        foreach (var kvp in cachedAttackSequence)
        {
            Debug.Log($"Part: {kvp.Key}, Number of unique combinations: {kvp.Value.Count}");
            for (int i = 0; i < kvp.Value.Count; i++)
            {
                string sequenceStr = string.Join(", ", kvp.Value[i].Select(x => x.ToString()));
                Debug.Log($"Combination {i}: {sequenceStr}");
            }
        }
    }
}
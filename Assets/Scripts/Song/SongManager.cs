using System.Collections.Generic;
using UnityEngine;
using System;

public class SongManager : MonoBehaviour
{
    [SerializeField] private List<PartOfTheSongSequence> partsOfTheSong;
    [SerializeField] private AudioSource musicSource;

    private PartOfTheSong currentSongPart;
    private int currentIndex = -1; // Start at -1 so first increment goes to 0

    public event Action OnManagerInitialized;
    public bool IsInitialized { get; private set; }

    void Start()
    {
        if (musicSource == null)
        {
            musicSource = GetComponent<AudioSource>();
        }

        ValidateAndPrepareSongSequence();

        // Initialize first part before any subscribers are notified
        InitializeFirstPart();

        // Mark as initialized and notify listeners
        IsInitialized = true;
        OnManagerInitialized?.Invoke();

        // Broadcast initial state
        if (currentSongPart != null)
        {
            OnPartChanged();
        }
    }

    private void InitializeFirstPart()
    {
        if (partsOfTheSong.Count > 0)
        {
            // Find the appropriate starting part based on current time
            float currentTime = musicSource.time;
            for (int i = 0; i < partsOfTheSong.Count; i++)
            {
                var part = partsOfTheSong[i];
                if (currentTime >= part.startTime && currentTime < part.endTime)
                {
                    currentIndex = i;
                    currentSongPart = part.songPart;
                    Debug.Log($"Initialized first song part to: {currentSongPart} at time: {currentTime}");
                    return;
                }
            }

            // If no matching part found, start with the first one
            currentIndex = 0;
            currentSongPart = partsOfTheSong[0].songPart;
            Debug.Log($"Defaulted to first song part: {currentSongPart}");
        }
        else
        {
            Debug.LogError("No song parts defined for initialization!");
        }
    }

    private void ValidateAndPrepareSongSequence()
    {
        if (partsOfTheSong.Count == 0)
        {
            Debug.LogError("No song parts defined!");
            return;
        }

        // Validate timing sequence
        for (int i = 0; i < partsOfTheSong.Count; i++)
        {
            var part = partsOfTheSong[i];

            // Ensure end time is after start time
            if (part.endTime <= part.startTime)
            {
                Debug.LogError($"Song part {i} has invalid timing: end time must be greater than start time");
            }

            // Check for gaps or overlaps with next part
            if (i < partsOfTheSong.Count - 1)
            {
                var nextPart = partsOfTheSong[i + 1];
                if (nextPart.startTime < part.endTime)
                {

                    Debug.LogWarning($"Overlap detected between parts {i} and {i + 1}");
                }
                else if (nextPart.startTime > part.endTime)
                {
                    Debug.LogWarning($"Gap detected between parts {i} and {i + 1}");
                }
            }
        }
    }

    private void Update()
    {
        if (!musicSource.isPlaying) return;
        float currentTime = musicSource.time;
        CheckAndUpdateCurrentPart(currentTime);
    }

    private void CheckAndUpdateCurrentPart(float currentTime)
    {
        // Find the correct part for the current time
        for (int i = 0; i < partsOfTheSong.Count; i++)
        {
            var part = partsOfTheSong[i];
            if (currentTime >= part.startTime && currentTime < part.endTime)
            {
                if (currentIndex != i)
                {
                    currentIndex = i;
                    SetCurrentSongPart();
                }
                return;
            }
        }
    }

    private void SetCurrentSongPart()
    {
        if (currentIndex >= 0 && currentIndex < partsOfTheSong.Count)
        {
            currentSongPart = partsOfTheSong[currentIndex].songPart;
            OnPartChanged();
            Debug.Log($"Current Song Part Changed to: {currentSongPart} at time: {musicSource.time}");
        }
    }

    private void OnPartChanged()
    {
        SongPartChangedEvent?.Invoke(currentSongPart);
    }

    // Public event that other components can subscribe to
    public event Action<PartOfTheSong> SongPartChangedEvent;

    // Public methods to query song state
    public PartOfTheSong GetCurrentPart() => currentSongPart;

    public float GetPartProgress()
    {
        if (currentIndex < 0 || currentIndex >= partsOfTheSong.Count) return 0f;

        var currentPart = partsOfTheSong[currentIndex];
        float partTime = musicSource.time - currentPart.startTime;
        float partDuration = currentPart.endTime - currentPart.startTime;
        return Mathf.Clamp01(partTime / partDuration);
    }
}

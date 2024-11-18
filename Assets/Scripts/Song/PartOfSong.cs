using System;

[Serializable]
public enum PartOfTheSong
{
    GuitarWithoutTechSound,
    GuitarWithTechSound,
    Bridge,
    TheRampageMoment,
    // ShotgunWithReloadMoment,
    // ShotgunWithoutReloadMoment,
    BigBridge,
    TheQuietBigBridge,
    TheEndingTitles,
    EndingBridge,
}


[Serializable]
public class PartOfTheSongSequence
{
    public float startTime;
    public float endTime;
    // public int timesPlayed;
    public PartOfTheSong songPart;
}
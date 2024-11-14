using System;
using System.Collections.Generic;

[Serializable]
public enum AttackType
{
    SingleHit,
    DoubleHit,
    Pass,
}

[Serializable]
public enum AttackStage
{
    Preparation,
    Hit,
    Cooldown,
}


[Serializable]
public class Attack
{
    public List<AttackStage> stages;
    public AttackType type;
}

// This is the one that you will use a lot guys
[Serializable]
public class AttackSequence
{
    public List<AttackType> sequence;
    public PartOfTheSong partOfTheSong;
}


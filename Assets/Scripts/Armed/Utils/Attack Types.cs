using System;
using System.Collections.Generic;

[Serializable]
public enum AttackType
{
    Pass,
    Preparation,
    Hit,
}


// This is the one that you will use a lot guys
[Serializable]
public class AttackSequence
{
    public List<AttackType> sequence;
    public PartOfTheSong partOfTheSong;
}


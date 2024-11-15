using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Windows.Speech;
using System.Linq;

public class AttackTypeManager : MonoBehaviour
{

    [SerializeField]
    private List<Attack> attacks;

    [SerializeField]
    [Tooltip("Required to set the attacks")]
    private List<AttackSequence> attackSequence;

    private List<AttackSequence> AttackSequences { get { return attackSequence; } }

    // I thought it is stubid to generate a sequence every time when a NPC is spawned
    private List<AttackSequence> CachedAttackSequence;

    private void InitAttackSequence()
    {

    }

    public List<AttackSequence> GenerateAttackSequence()
    {
        System.Random rnd = new System.Random();
        return attackSequence.OrderBy(x => rnd.Next()).ToList();
    }


}

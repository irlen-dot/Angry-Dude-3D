using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class AttackTypeManager : MonoBehaviour
{

    [SerializeField]
    private List<Attack> attacks;

    [SerializeField]
    [Tooltip("Required to set the attacks")]
    private List<AttackSequence> attackSequence;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

using UnityEngine;

public class Hit : MonoBehaviour
{
    private ArmedNPC armedNPC;

    private int numberOfNPCsInside = 0;
    private bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        armedNPC = null;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse was triggered.");
            isHit = true;
        }
    }

    void FixedUpdate()
    {
        if (isHit)
        {
            ProcessHit();
        }
    }

    private void ProcessHit()
    {
        if (armedNPC == null)
        {
            return;
        }
        armedNPC.ProcessDamage();
        isHit = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Armed NPC"))
        {
            if (numberOfNPCsInside < 1)
            {
                armedNPC = other.GetComponent<ArmedNPC>();
                Debug.Log("Armed NPC in the Hit range was set.");
            }
            numberOfNPCsInside++;
            Debug.Log($"NPCs to hit: {numberOfNPCsInside}.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Armed NPC"))
        {
            numberOfNPCsInside--;
            if (numberOfNPCsInside <= 0)
            {
                armedNPC = null;
                Debug.Log("Armed NPC in the Hit range was unset.");
            }
            Debug.Log($"NPCs to hit: {numberOfNPCsInside}.");

        }
    }
}

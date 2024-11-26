using UnityEngine;

public class Hit : MonoBehaviour
{
    private ArmedNPC armedNPC;
    private Door door;

    private int numberOfNPCsInside = 0;
    private bool isHit;

    private bool canBreakDoor;

    public bool CanBreakDoor { set { canBreakDoor = value; } }

    // Start is called before the first frame update
    void Start()
    {
        canBreakDoor = false;
        isHit = false;
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

    public void LogSome()
    {
        Debug.Log("Log some.");
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
        Debug.Log($"The Can Break Door prop: {door}");
        if (armedNPC)
        {
            Debug.Log("Hit Npc hard.");
            HitArmedNpc();
        }
        else if (door && canBreakDoor)
        {
            Debug.Log("Door Breaking");
            HitDoor();
        }
    }

    private void HitDoor()
    {
        door.BreakDoor();
    }

    private void HitArmedNpc()
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
            OnNPCEnter(other);
        }
        if (other.tag.Equals("Door"))
        {
            Debug.Log("Door added.");
            door = other.GetComponent<Door>();
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Armed NPC"))
        {
            OnNPCExit();
        }
    }


    private void OnNPCEnter(Collider other)
    {
        if (numberOfNPCsInside < 1)
        {
            armedNPC = other.GetComponent<ArmedNPC>();
            Debug.Log("Armed NPC in the Hit range was set.");
        }
        numberOfNPCsInside++;
        Debug.Log($"NPCs to hit: {numberOfNPCsInside}.");

    }

    private void OnNPCExit()
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

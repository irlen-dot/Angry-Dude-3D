using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    private ArmedNPC armedNPC;

    private List<ArmedNPC> nPCsInside = new List<ArmedNPC>();

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

    void FixedUpdate()
    {
        if (isHit)
        {
            ProcessHit();
        }
    }

    private void ProcessHit()
    {
        Debug.Log("Initing the hit...");
        if (armedNPC)
        {
            HitArmedNpc();
        }
        else if (door && canBreakDoor)
        {
            HitDoor();
        }
        isHit = false;
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Armed NPC"))
        {
            OnNPCEnter(other);
        }
        if (other.tag.Equals("Door"))
        {
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
        }
        nPCsInside.Add(armedNPC);
        numberOfNPCsInside++;
    }

    private void OnNPCExit()
    {
        numberOfNPCsInside--;
        if (numberOfNPCsInside <= 0)
        {
            armedNPC = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private List<ShootSequence> shootSequences = new List<ShootSequence>();
    [SerializeField] private float spreadAngle = 20f;
    [SerializeField] private int pelletCount = 8;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 10f;
    private int currentSequenceIndex = 0;
    private int currentisShootIndex = 0;

    private bool currentIsShooting;

    private Shotgun shotgun;

    private bool isButtonPushed = false;


    void Awake()
    {
        shotgun = FindFirstObjectByType<Shotgun>();
    }

    void OnEnable()
    {
        shotgun = FindFirstObjectByType<Shotgun>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isButtonPushed = true;
        }
    }

    void FixedUpdate()
    {
        ShootCycle();
        if (isButtonPushed) isButtonPushed = false;
    }

    void ShootCycle()
    {
        if (currentSequenceIndex >= shootSequences.Count - 1)
        {
            currentSequenceIndex = 0;
        }
        if (currentisShootIndex >= shootSequences[currentSequenceIndex].isShootSequence.Count - 1)
        {
            currentSequenceIndex++;
            currentisShootIndex = 0;
        }
        else
        {
            currentisShootIndex++;
        }
        ShootSequence shootSequence = shootSequences[currentSequenceIndex];

        currentIsShooting = shootSequence.isShootSequence[currentisShootIndex];

        ProcessShooting();

    }

    void ProcessShooting()
    {
        if (!isButtonPushed) return;
        if (currentIsShooting)
        {
            FireShotgun();
        }
        else
        {
            shotgun.Reload();
        }

    }



    void FireShotgun()
    {
        float sphereRadius = 0.5f; // Adjust based on desired spread

        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 spreadDirection = transform.forward;
            spreadDirection = Quaternion.AngleAxis(
                Random.Range(-spreadAngle, spreadAngle),
                transform.up) * spreadDirection;
            spreadDirection = Quaternion.AngleAxis(
                Random.Range(-spreadAngle, spreadAngle),
                transform.right) * spreadDirection;

            RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereRadius, spreadDirection, range);

            foreach (RaycastHit hit in hits)
            {
                ArmedNPC npc = hit.collider.GetComponent<ArmedNPC>();
                if (npc != null)
                {
                    npc.ProcessShotgunDamage();
                }
            }
        }
    }

    void Animate()
    {

    }
}
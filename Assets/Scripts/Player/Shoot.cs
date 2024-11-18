using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float spreadAngle = 20f;
    [SerializeField] private int pelletCount = 8;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireShotgun();
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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVFX : MonoBehaviour
{
    private ParticleSystem meatVFX;
    private ParticleSystem bloodVFX;

    // Start is called before the first frame update
    void Start()
    {
        bloodVFX = transform.Find("Blood Particles").GetComponent<ParticleSystem>();
        meatVFX = transform.Find("Meat Particles").GetComponent<ParticleSystem>();
    }

    public void StartVFX()
    {
        bloodVFX.Play();
        meatVFX.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

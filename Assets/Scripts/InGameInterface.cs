using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameInterface : MonoBehaviour
{
    private TMP_Text healthText;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void InitAllTexts()
    {
        healthText = transform.Find("Health text").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHealth(int healthLeft)
    {
        healthText.text = $"Number of health left {healthLeft}";
    }
}

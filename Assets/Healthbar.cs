using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Healthbar : MonoBehaviour
{
    public Slider healthBar;
    public Hunger playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Hunger>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.getMaxValue();
        playerHealth.getMaxValue();
    }

    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }
}

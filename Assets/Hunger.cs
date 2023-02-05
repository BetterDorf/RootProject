using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    [SerializeField] float maxValue;
    [SerializeField] float decreaseValue = 1;
    [SerializeField] float hungerRate;
    [SerializeField] Healthbar healthBar;
    [SerializeField] private PlayerVisuals playerVisuals;
    [SerializeField] private float invincibilityTime;
    private float energyValue;
    private float hungerDecreaseTime = 0.0f;
    private float invincibility = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (healthBar == null)
        {
            Debug.Log("No hungerbar set for Hunger. Disabling Hunger component");
            enabled = false;
        }

        energyValue = maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        hungerDecreaseTime += Time.deltaTime;
        this.overtimeDecrease();
        healthBar.SetHealth(hp: energyValue);
        this.testDamage();

        if (invincibility > 0.0f)
        {
            invincibility -= Time.deltaTime;
        }
    }

    private void overtimeDecrease()
    {
        //print(message: $"OVERTIME DECREASE {hungerDecreaseTime}");
        if (hungerDecreaseTime>hungerRate)
        {
            hungerDecreaseTime = 0.0f;
            energyValue = energyValue - decreaseValue;
            healthBar.SetHealth(energyValue);
        }
    }

    public void restoreEnergy(int value) 
    {
        float res = this.energyValue + value;
        if (res > maxValue)
        {
            energyValue = maxValue;
        }
        else
        {
            energyValue = res;
        }
    }

    public void removeEnergy(float value)
    {
        if (invincibility > 0.0f)
        {
            return;
        }

        float res = this.energyValue - value;
        if (res < 0)
        {
            energyValue = 0;
        }
        else
        {
            energyValue = res;
        }

        invincibility = invincibilityTime;
        playerVisuals.StartCoroutine(playerVisuals.Flicker(invincibilityTime));
    }

    public bool IsDead()
    {
        return energyValue <= 0;
    }

    private void Die()
    {
        // TODO end level
    }

    public float getMaxValue()
    {
        return maxValue;
    }

    private void testDamage() {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            removeEnergy(10);
            healthBar.SetHealth(energyValue);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            restoreEnergy(10);
            healthBar.SetHealth(energyValue);
        }
    }
  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    [SerializeField] float maxValue;
    [SerializeField] float decreaseValue = 1;
    [SerializeField] int hungerRate;
    [SerializeField] Healthbar healthBar;
    private float energyValue;
    private int frameCounter= 0;



    // Start is called before the first frame update
    void Start()
    {
        energyValue = maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        frameCounter++;
        this.overtimeDecrease();
        this.deathSignal();
        healthBar.SetHealth(hp: energyValue);
        this.testDamage();
    }

    private void overtimeDecrease()
    {
        print(message: $"OVERTIME DECREASE {frameCounter}");
        if (frameCounter.Equals(hungerRate))
        {
            frameCounter = 0;
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
        float res = this.energyValue - value;
        if (res < 0)
        {
            energyValue = 0;
        }
        else
        {
            energyValue = res;
        }
    }

    public bool deathSignal()
    {
        return energyValue <= 0;
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

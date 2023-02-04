using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    [SerializeField] float maxValue;
    [SerializeField] float energyValue;
    [SerializeField] float decreaseValue = 1;
    [SerializeField] int hungerRate;

    private int frameCounter;



    // Start is called before the first frame update
    void Start()
    {
        frameCounter = 0;
        energyValue = maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        this.overtimeDecrease();
        this.deathSignal();
    }

    private void overtimeDecrease()
    {
        frameCounter = frameCounter++;
        if (frameCounter.Equals(hungerRate))
        {
            frameCounter = 0;
            energyValue = energyValue - decreaseValue;
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

    public void removeEnergy(int value)
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
  
}

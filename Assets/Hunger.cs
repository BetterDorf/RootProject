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
    [SerializeField] private float killHeight;
    [SerializeField] private GameObject deathPanel;
    private float energyValue;
    private float hungerDecreaseTime = 0.0f;
    private float invincibility = 0.0f;

    public bool IsDead { get; private set; } = false;

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

        if (transform.position.y < killHeight)
        {
            Die();
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

    public void removeEnergy(float value, bool damage = true)
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

        if (damage)
        {
            invincibility = invincibilityTime;
            playerVisuals.StartCoroutine(playerVisuals.Flicker(invincibilityTime - 0.05f));
        }

        if (CheckDead())
        {
            Die();
        }
    }

    public bool CheckDead()
    {
        if (energyValue <= 0)
        {
            IsDead = true;
        }

        return IsDead;
    }

    private void Die()
    {
        IsDead = true;
        deathPanel.SetActive(true);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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

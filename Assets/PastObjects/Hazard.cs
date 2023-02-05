using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float knocbackForce;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Hunger playerHunger = collider2D.GetComponent<Hunger>();

        if (!playerHunger)
        {
            return;
        }

        playerHunger.removeEnergy(damage);
        collider2D.attachedRigidbody.velocity = (collider2D.transform.position - transform.position).normalized * knocbackForce;
    }
}

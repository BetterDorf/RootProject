using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : Interactable
{
    [SerializeField] int healthValue;
    

    public override void Interact(GameObject playerGameObject) 
    { 
        playerGameObject.GetComponent<Hunger>().restoreEnergy(10);

    }
}

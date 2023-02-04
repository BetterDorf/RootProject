using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Interactable
{
    public override void Interact(GameObject playerGameObject)
    {
        base.Interact(playerGameObject);

        Debug.Log("ExitTriggered");
    }
}

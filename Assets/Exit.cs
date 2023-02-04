using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Interactable
{
    public override void Interact()
    {
        base.Interact();

        Debug.Log("ExitTriggered");
    }
}

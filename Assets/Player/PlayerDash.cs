using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [SerializeField]

    public void DashAction(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.started)
        {
            return;
        }
    }
}

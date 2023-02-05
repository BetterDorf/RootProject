using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SummonMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private GameObject instMenu = null;

    public void SummonMenuAction(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.started)
        {
            return;
        }

        if (instMenu == null)
        {
            instMenu = Instantiate(menu);
            Time.timeScale = 0.0f;
        }
        else
        {
            Destroy(instMenu);
            instMenu = null;
            Time.timeScale = 1.0f;
        }
    }
}

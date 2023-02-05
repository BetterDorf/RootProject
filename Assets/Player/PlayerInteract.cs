using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Hunger playerHunger;
    private Interactable _currentInteractable = null;

    public void SetInteractable(Interactable interactable)
    {
        _currentInteractable = interactable;
    }

    public void RemoveInteractable(Interactable interactable)
    {
        if (_currentInteractable == interactable)
        {
            _currentInteractable = null;
        }
    }

    public void InteractAction(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.started || !_currentInteractable || playerHunger.IsDead)
        {
            return;
        }

        _currentInteractable.Interact(this.gameObject);
    }
}

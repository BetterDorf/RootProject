using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(ButtonPrompt))]
public class Interactable : MonoBehaviour
{
    private ButtonPrompt[] buttonPrompts;

    void Start()
    {
        buttonPrompts = GetComponents<ButtonPrompt>();
    }

    public virtual void Interact(GameObject playerGameObject)
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInteract player = collision.GetComponent<PlayerInteract>();
        if (player)
        {
            player.SetInteractable(this);

            foreach (var buttonPrompt in buttonPrompts)
            {
                buttonPrompt.FadeIn();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInteract player = collision.GetComponent<PlayerInteract>();
        if (player)
        {
            player.RemoveInteractable(this);

            foreach (var buttonPrompt in buttonPrompts)
            {
                buttonPrompt.FadeOut();
            }
        }
    }
}

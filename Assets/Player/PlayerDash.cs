using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;
    private bool dashing = false;

    void Update()
    {
        if (!dashing)
        {
            return;
        }

        dashTime += Time.deltaTime;
    }

    public void DashAction(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.started)
        {
            return;
        }

        dashTime = 0.0f;
        playerMovement.canMove = false;

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * dashSpeed;
    }
}

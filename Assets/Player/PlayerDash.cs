using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerVisuals playerVisuals;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashEndSpeed = 3.0f;
    [SerializeField] private float dashDuration;
    private float dashTime;
    private bool dashing = false;

    private bool canDash = false;

    private float prevGravity = 0.0f;

    public void StartDash()
    {
        rb.velocity = dashSpeed * (playerVisuals.FacingRight ? Vector2.right : Vector2.left);
    }

    void Update()
    {
        if (playerMovement.AirStatus == AirStatus.Grounded)
        {
            canDash = true;
        }

        if (!dashing)
        {
            return;
        }

        rb.velocity = new Vector2(dashSpeed * (playerVisuals.FacingRight ? 1.0f : -1.0f), 0.0f);
        dashTime += Time.deltaTime;

        if (dashTime <= dashDuration)
        {
            return;
        }

        dashing = false;
        rb.velocity = new Vector2(dashEndSpeed * (playerVisuals.FacingRight ? 1.0f : -1.0f), 0.0f);
        rb.gravityScale += prevGravity;
        playerMovement.canMove = true;
    }

    public void DashAction(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.started || dashing || !canDash)
        {
            return;
        }

        playerVisuals.Special();

        dashing = true;
        canDash = false;

        dashTime = 0.0f;
        playerMovement.canMove = false;

        prevGravity = rb.gravityScale;
        rb.gravityScale = 0.0f;
        rb.velocity = Vector2.zero;
    }
}

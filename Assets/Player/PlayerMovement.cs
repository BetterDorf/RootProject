using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    internal enum AirStatus : int
    {
        Grounded = 0,
        Rising = 1,
        Falling = 2
    }

    public class PlayerMovement : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject groundOrigin;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private LayerMask groundLayers;

        private AirStatus airStatus;

        [Header("MovementVariables")]
        [Header("Jump")] 
        [SerializeField] private float jumpVel = 0.0f;
        [SerializeField] private float minTimeBetweenJump;
        [Tooltip("Time the button must be held for big jump")] 
        [SerializeField] private float jumpBoostTime;
        [SerializeField] private float jumpBoostVel;

        private float lastJumpTime = 0.0f;
        private bool hadBoost = false;
        private bool jumpHeld = false;

        private float moveDir = 0.0f;
        private bool moving = false;

        [Header("HorizontalMovement")]
        [SerializeField] private float horGroundAccel;
        [SerializeField] private float horAirAccel;
        [SerializeField] private float horMaxSpeed;

        // Start is called before the first frame update
        void Start()
        {

        }

        void Update()
        {
            lastJumpTime += Time.deltaTime;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            var hit2D = Physics2D.OverlapBox(groundOrigin.transform.position,
                Vector2.one * 0.05f, 0.0f, groundLayers);
            if (hit2D)
            {
                airStatus = AirStatus.Grounded;
            }  
            else if (rb.velocity.y > 0.0f)
            {
                airStatus = AirStatus.Rising;
            }
            else
            {
                airStatus = AirStatus.Falling;
            }

            // Gives jump boost if high jump wasn't canceled
            if (jumpHeld && !hadBoost && lastJumpTime > jumpBoostTime)
            {
                rb.velocity += new Vector2(0.0f, jumpBoostVel);
                hadBoost = true;
            }

            // Handle horizontal movement
            if (!moving)
            {
                // Deccel
                // TODO deccel only the part from the movement
                rb.velocity = new Vector2(0.0f, rb.velocity.y);
                return;
            }

            var accel = airStatus == AirStatus.Grounded ? horGroundAccel : horAirAccel;

            if (Mathf.Abs(rb.velocity.x) > horMaxSpeed && Mathf.Sign(moveDir * rb.velocity.x) >= 0.0f)
            {
                return;
            }

            // Accelerate
            rb.velocity += new Vector2(moveDir * accel * Time.fixedDeltaTime, 0.0f);

            if (Mathf.Abs(rb.velocity.x) > horMaxSpeed && Mathf.Sign(moveDir * rb.velocity.x) >= 0.0f)
            {
                rb.velocity = new Vector2(horMaxSpeed * moveDir, rb.velocity.y);
            }
        }

        public void JumpAction(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started && airStatus == AirStatus.Grounded)
            {
                if (!(lastJumpTime > minTimeBetweenJump))
                {
                    return;
                }

                lastJumpTime = 0.0f;

                // set jump vel
                rb.velocity += new Vector2(0.0f, jumpVel);

                hadBoost = false;
                jumpHeld = true;
            }
            else if (callbackContext.canceled)
            {
                jumpHeld = false;
            }
        }

        public void MoveAction(InputAction.CallbackContext callbackContext)
        {
            Vector2 value = callbackContext.ReadValue<Vector2>();

            if (callbackContext.canceled || value == Vector2.zero)
            {
                moving = false;
                return;
            }

            moving = true;
            moveDir = Mathf.Sign(value.x);
        }
    }

}
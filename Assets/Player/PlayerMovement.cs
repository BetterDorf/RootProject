using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public enum AirStatus : int
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
        [SerializeField] private PlayerVisuals playerVisuals;

        public AirStatus AirStatus { get; set; }

        [Header("MovementVariables")]
        [SerializeField] private Vector2 groundCheckSize;
        [SerializeField] private float coyoteTime;
        [SerializeField] private float startFallingAfter;

        private float coyoteTimeLeft;
        private float fallingTime = 0.0f;

        [Header("Jump")] 
        [SerializeField] private float jumpVel = 0.0f;
        [SerializeField] private float minTimeBetweenJump;
        [Tooltip("Time the button must be held for big jump")] 
        [SerializeField] private float jumpBoostTime;
        [SerializeField] private float jumpBoostVel;

        private float lastJumpTime = 0.0f;
        private bool hadBoost = false;
        private bool jumpHeld = false;

        [Header("HorizontalMovement")]
        [SerializeField] private float horGroundAccel;
        [SerializeField] private float horAirAccel;
        [SerializeField] private float horMaxSpeed;

        private float moveDir = 0.0f;
        public bool Moving { get; private set; } = false;

        public bool canMove = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        void Update()
        {
            lastJumpTime += Time.deltaTime;
            coyoteTimeLeft -= Time.deltaTime;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // Ground Check
            var hit2D = Physics2D.OverlapBox(groundOrigin.transform.position,
                groundCheckSize, 0.0f, groundLayers);
            if (hit2D || coyoteTimeLeft > 0.0f)
            {
                if (AirStatus != AirStatus.Grounded)
                {
                     playerVisuals.Land();
                }

                AirStatus = AirStatus.Grounded;
                fallingTime = 0.0f;

                if (hit2D)
                {
                    var groundBody = hit2D.GetComponent<Rigidbody2D>();
                    if (groundBody && !jumpHeld)
                    {
                        transform.parent = hit2D.transform;
                    }
                    coyoteTimeLeft = coyoteTime;
                }
            }  
            else if (rb.velocity.y > 0.0f)
            {
                AirStatus = AirStatus.Rising;
            }
            else
            {
                fallingTime += Time.fixedDeltaTime;

                if (AirStatus == AirStatus.Grounded)
                {
                    if (fallingTime > startFallingAfter)
                    {
                        AirStatus = AirStatus.Falling;
                    }
                }
                else
                {
                    AirStatus = AirStatus.Falling;
                }
            }

            // Gives jump boost if high jump wasn't canceled
            if (jumpHeld && !hadBoost && lastJumpTime > jumpBoostTime && canMove)
            {
                rb.velocity += new Vector2(0.0f, jumpBoostVel);
                hadBoost = true;
            }

            // Handle horizontal movement
            if (!canMove)
            {
                return;
            }

            if (!Moving)
            {
                // Deccel
                // TODO deccel only the part from the movement
                rb.velocity = new Vector2(0.0f, rb.velocity.y);
                return;
            }

            var accel = AirStatus == AirStatus.Grounded ? horGroundAccel : horAirAccel;

            // if going too fast
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
            if (callbackContext.started && AirStatus == AirStatus.Grounded && canMove)
            {
                if (!(lastJumpTime > minTimeBetweenJump))
                {
                    return;
                }

                playerVisuals.StartJump();

                lastJumpTime = 0.0f;
                coyoteTimeLeft = -1.0f;

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

            if (callbackContext.canceled || value.x == 0.0f)
            {
                Moving = false;
                return;
            }

            Moving = true;
            moveDir = Mathf.Sign(value.x);

            if (canMove)
            {
                playerVisuals.ChangeOrientation(moveDir >= 1.0f);
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] private GameObject groundOrigin;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private LayerMask groundLayers;

        private AirStatus airStatus;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Physics2D.Raycast(groundOrigin.transform.position, Vector2.down,0.05f, groundLayers))
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

            if (airStatus == AirStatus.Falling)
            {
                Debug.Log("Falling");
            }
        }
    }

}
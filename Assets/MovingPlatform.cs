using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 movementVector3;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private bool moveTowardsEnd = true;

    void Start()
    {
        startPoint = transform.position;
        endPoint = startPoint + movementVector3;
    }

    void FixedUpdate()
    {
        var sign = moveTowardsEnd ? 1.0f : -1.0f;
        var goalPoint = moveTowardsEnd ? endPoint : startPoint;

        rb.velocity = movementVector3.normalized * sign * moveSpeed;

        if (Vector3.Dot((transform.position - goalPoint), movementVector3 * sign) > 0.00f)
        {
            transform.position = goalPoint;
            moveTowardsEnd = !moveTowardsEnd;
        }
    }
}

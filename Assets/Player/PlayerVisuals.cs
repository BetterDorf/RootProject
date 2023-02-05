using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Color transparencyColor;
    [SerializeField] private float flickerSpeed;
    private float offset;

    private bool flickering = false;

    void Start()
    {
        offset = transform.localPosition.x;
    }

    void Update()
    {
        animator.SetBool("moving", playerMovement.Moving);
        animator.SetInteger("AirStatus", (int) playerMovement.AirStatus);
    }

    public void StartDash()
    {
        transform.parent.GetComponent<PlayerDash>().StartDash();
    }

    public void Special()
    {
        animator.SetTrigger("Special");
    }

    public void Land()
    {
        animator.SetTrigger("Land");
    }

    public void StartJump()
    {
        animator.SetTrigger("Jump");
    }

    public bool FacingRight { get; private set; } = true;
    public void ChangeOrientation(bool facingRight)
    {
        FacingRight = facingRight;
        transform.rotation = Quaternion.Euler(0.0f, facingRight ? 0.0f : 180.0f, 0.0f);
        transform.localPosition = new Vector3(facingRight ? offset : -offset, transform.localPosition.y,
            transform.localPosition.z);
    }

    public IEnumerator Flicker(float flickerTime)
    {
        if (flickering)
        {
            yield break;
        }

        flickering = true;
        float flickerTimeLeft = flickerTime;
        float timeToNextFlicker = 1 / flickerSpeed;

        bool visible = true;

        while(flickerTimeLeft > 0.0f)
        {
            if (timeToNextFlicker <= 0.0f)
            {
                visible = !visible;
                sp.color = visible ? Color.white : transparencyColor;
                timeToNextFlicker = 1 / flickerSpeed;
            }

            yield return null;

            flickerTimeLeft -= Time.deltaTime;
            timeToNextFlicker -= Time.deltaTime;
        }

        sp.color = Color.white;
        flickering = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    public bool FacingRight { get; private set; } = true;
    public void ChangeOrientation(bool facingRight)
    {
        FacingRight = facingRight;
        transform.rotation = Quaternion.Euler(0.0f, facingRight ? 0.0f : 180.0f, 0.0f);
    }
}

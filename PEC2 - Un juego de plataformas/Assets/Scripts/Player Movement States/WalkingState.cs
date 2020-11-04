using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerMovementState
{
    public float deadZone = 0.80f;
    public void FixedUpdate()
    {
        if (playerController.horizontalInput < -deadZone)
        {
            rb.velocity = new Vector2(-playerController.maxWalkingSpeed, rb.velocity.y);
        }
        else if (playerController.horizontalInput > deadZone)
        {
            rb.velocity = new Vector2(playerController.maxWalkingSpeed, rb.velocity.y);
        }
        else rb.velocity = new Vector2(0f, rb.velocity.y);
    }
}

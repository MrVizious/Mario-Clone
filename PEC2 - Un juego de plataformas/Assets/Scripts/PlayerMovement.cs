using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput input;

    public float groundHorizontalAcceleration = 500f, airHorizontalAcceleration = 200f, maxHorizontalSpeed = 3.7f;
    public float groundCheckDistance = 0.05f, jumpForce = 17.36f;
    public float normalGravity = 6.9f, slowGravity = 3.5f;
    public bool grounded = false;
    private int groundCheckLayermask;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        groundCheckLayermask = 1 << LayerMask.NameToLayer("Foreground");
    }

    private void Update()
    {
        //Jump action
        if (input.jumpInputPressedDown && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            grounded = false;
        }

        //Add speed if moving on the ground
        if (input.horizontalInput != 0f)
        {
            if (grounded) rb.velocity = new Vector2(rb.velocity.x + input.horizontalInput * groundHorizontalAcceleration * Time.deltaTime, rb.velocity.y);
            else rb.velocity = new Vector2(rb.velocity.x + input.horizontalInput * airHorizontalAcceleration * Time.deltaTime, rb.velocity.y);
        }
    }
    void FixedUpdate()
    {
        //Check ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -rb.transform.up, groundCheckDistance, groundCheckLayermask);
        Debug.DrawRay(transform.position, -rb.transform.up * groundCheckDistance, Color.red);
        if (hit.collider != null) grounded = hit.collider.tag.Equals("Terrain");
        else grounded = false;

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed), rb.velocity.y);

        //If the user is holding down the jump button and the character isn't
        //falling down, the gravity should be lower
        if (input.jumpInputHeldDown && rb.velocity.y > 0f)
        {
            rb.gravityScale = slowGravity;
        }
        else rb.gravityScale = normalGravity;
    }

}
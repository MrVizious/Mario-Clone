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
    public float groundCheckOffset = 0.625f;
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

        //Debug.Log("X velocity: " + rb.velocity.x);

        //Add speed if on the ground
        if (grounded) rb.velocity = new Vector2(rb.velocity.x + input.horizontalInput * groundHorizontalAcceleration * Time.deltaTime, rb.velocity.y);
        else rb.velocity = new Vector2(rb.velocity.x + input.horizontalInput * airHorizontalAcceleration * Time.deltaTime, rb.velocity.y);
    }
    void FixedUpdate()
    {
        //Check ground
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - ((Vector3)Vector2.right * groundCheckOffset), -rb.transform.up, groundCheckDistance, groundCheckLayermask);
        Debug.DrawRay(transform.position - ((Vector3)Vector2.right * groundCheckOffset), -rb.transform.up * groundCheckDistance, Color.red);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + ((Vector3)Vector2.right * groundCheckOffset), -rb.transform.up, groundCheckDistance, groundCheckLayermask);
        Debug.DrawRay(transform.position + ((Vector3)Vector2.right * groundCheckOffset), -rb.transform.up * groundCheckDistance, Color.red);

        bool groundedLeft = false;
        bool groundedRight = false;

        if (hitLeft.collider != null) groundedLeft = hitLeft.collider.tag.Equals("Terrain") || hitLeft.collider.tag.Equals("Block");
        if (hitRight.collider != null) groundedRight = hitRight.collider.tag.Equals("Terrain") || hitRight.collider.tag.Equals("Block");
        grounded = groundedLeft || groundedRight;

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
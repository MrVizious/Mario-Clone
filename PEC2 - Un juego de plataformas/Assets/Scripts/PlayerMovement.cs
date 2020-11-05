using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput input;

    public float horizontalForce = 2f, maxHorizontalSpeed = 9f;
    public float groundCheckDistance = 0.05f, jumpForce = 350f;
    public bool grounded = false;
    private int groundCheckLayermask;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        groundCheckLayermask = 1 << LayerMask.NameToLayer("Foreground");
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -rb.transform.up, groundCheckDistance, groundCheckLayermask);
        Debug.DrawRay(transform.position, -rb.transform.up * groundCheckDistance, Color.red);
        if (hit.collider != null) grounded = hit.collider.tag.Equals("Terrain");
        else grounded = false;
        Debug.Log("Grounded: " + grounded);

        rb.AddForce(Vector2.right * horizontalForce * Time.fixedDeltaTime * input.horizontalInput);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed), rb.velocity.y);
        if (input.jumpInputPressedDown && grounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
}
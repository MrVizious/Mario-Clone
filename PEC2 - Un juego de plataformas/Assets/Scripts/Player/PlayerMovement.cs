using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput input;

    public float groundHorizontalAcceleration = 500f, airHorizontalAcceleration = 200f, maxHorizontalSpeed = 3.7f;
    public float groundCheckDistance = 0.05f, jumpForce = 17.36f, groundCheckOffset = 0.3f;
    public float enemyCheckDistance = 0.05f, enemyCheckOffset = 0.3f;
    public float normalGravity = 6.9f, slowGravity = 3.5f;
    public bool grounded = false;

    public GameObject fireballPrefab;
    private int groundCheckLayermask, enemyCheckLayerMask;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        groundCheckLayermask = 1 << LayerMask.NameToLayer("Foreground");
        enemyCheckLayerMask = 1 << LayerMask.NameToLayer("Enemy");
    }

    private void Update()
    {
        CheckJump();
        HorizontalMovement();
        CheckFireball();
    }
    void FixedUpdate()
    {
        IsGrounded();
        CheckEnemyBelow();

        AdjustGravity();
        ClampVelocity();
    }

    /// <summary>
    /// Checks if the player should jump, and executes it if so
    /// </summary>
    public void CheckJump()
    {
        //Jump action
        if (input.jumpInputPressedDown && grounded)
        {
            Jump();
            grounded = false;
        }
    }

    /// <summary>
    /// Executes jump without any condition
    /// </summary>
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void CheckFireball()
    {
        if (input.runInputPressedDown) Fire();
    }

    private void Fire()
    {
        Instantiate(fireballPrefab, transform.position,
                    GetComponent<SpriteRenderer>().flipX ?
                        Quaternion.Euler(0, 180, 0)
                        : Quaternion.Euler(0, 0, 0)
                    );
    }

    /// <summary>
    /// Adjusts the gravity based on whether the jump button is still pressed
    /// and if the player is falling
    /// </summary>
    private void AdjustGravity()
    {
        //If the user is holding down the jump button and the character isn't
        //falling down, the gravity should be lower
        if (input.jumpInputHeldDown && rb.velocity.y > 0f)
        {
            rb.gravityScale = slowGravity;
        }
        else rb.gravityScale = normalGravity;
    }

    /// <summary>
    /// Moves the player depending on the horizontal input.
    /// Speed is different when grounded or on air.
    /// </summary>
    public void HorizontalMovement()
    {
        //Add speed if on the ground
        if (grounded) rb.velocity = new Vector2(rb.velocity.x + input.horizontalInput * groundHorizontalAcceleration * Time.deltaTime, rb.velocity.y);
        else rb.velocity = new Vector2(rb.velocity.x + input.horizontalInput * airHorizontalAcceleration * Time.deltaTime, rb.velocity.y);
    }

    /// <summary>
    /// Checks whether the player is grounded or not
    /// </summary>
    private bool IsGrounded()
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
        return grounded;
    }

    /// <summary>
    /// Checks if there is an enemy below, and applies a jump to the player while killing the enemy
    /// </summary>
    private void CheckEnemyBelow()
    {
        //Check enemy
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - ((Vector3)Vector2.right * enemyCheckOffset), -rb.transform.up, enemyCheckDistance, enemyCheckLayerMask);
        Debug.DrawRay(transform.position - ((Vector3)Vector2.right * enemyCheckOffset), -rb.transform.up * enemyCheckDistance, Color.red);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + ((Vector3)Vector2.right * enemyCheckOffset), -rb.transform.up, enemyCheckDistance, enemyCheckLayerMask);
        Debug.DrawRay(transform.position + ((Vector3)Vector2.right * enemyCheckOffset), -rb.transform.up * enemyCheckDistance, Color.red);
        RaycastHit2D hitCenter = Physics2D.Raycast(transform.position, -rb.transform.up, enemyCheckDistance, enemyCheckLayerMask);
        Debug.DrawRay(transform.position, -rb.transform.up * enemyCheckDistance, Color.red);

        if (hitLeft.collider != null && hitLeft.collider.tag.Equals("Enemy")) hitLeft.collider.gameObject?.GetComponent<Enemy>().Die(this);
        else if (hitRight.collider != null && hitRight.collider.tag.Equals("Enemy")) hitRight.collider.gameObject?.GetComponent<Enemy>().Die(this);
        else if (hitCenter.collider != null && hitCenter.collider.tag.Equals("Enemy")) hitCenter.collider.gameObject?.GetComponent<Enemy>().Die(this);
    }

    /// <summary>
    /// Ensures that the player doesn't surpass the maximum horizontal speed
    /// </summary>
    private void ClampVelocity()
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed), rb.velocity.y);
    }
}
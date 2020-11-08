using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaMovement : MonoBehaviour
{
    public bool startFacingRight = false;
    public float speed = 2f;
    public float collisionCheckDistance = 0.5f;
    public LayerMask collisionLayerMask;
    public LayerMask playerLayerMask;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Set initial speed to appropriate value
        speed = Mathf.Abs(speed) * (startFacingRight ? 1f : -1f);
    }

    private void Update()
    {

        CheckForWalls();
        CheckForPlayer();
        rb.velocity = new Vector2(speed, rb.velocity.y);

    }

    private void CheckForWalls()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (speed > 0f ? Vector2.right : Vector2.left), collisionCheckDistance, collisionLayerMask);
        Debug.DrawRay(transform.position, (speed > 0f ? Vector2.right : Vector2.left) * collisionCheckDistance);

        //Turn around if hitting a wall
        if (hit.collider != null) speed = -speed;
    }

    private void CheckForPlayer()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, collisionCheckDistance, playerLayerMask);
        Debug.DrawRay(transform.position, (speed > 0f ? Vector2.right : Vector2.left) * collisionCheckDistance);

        if (hitLeft.collider != null) hitLeft.collider.gameObject.GetComponent<PlayerAnimation>().Die();

        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, collisionCheckDistance, playerLayerMask);
        Debug.DrawRay(transform.position, (speed > 0f ? Vector2.right : Vector2.left) * collisionCheckDistance);

        if (hitRight.collider != null) hitRight.collider.gameObject.GetComponent<PlayerAnimation>().Die();
    }
}

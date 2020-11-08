using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaMovement : MonoBehaviour
{
    public bool startFacingRight = false;
    public float speed = 2f;
    public float collisionCheckDistance = 0.5f;
    public LayerMask collisionLayerMask;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Set initial speed to appropriate value
        speed = Mathf.Abs(speed) * (startFacingRight ? 1f : -1f);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (speed > 0f ? Vector2.right : Vector2.left), collisionCheckDistance, collisionLayerMask);
        Debug.DrawRay(transform.position, (speed > 0f ? Vector2.right : Vector2.left) * collisionCheckDistance);

        //Turn around if hitting a wall
        if (hit.collider != null) speed = -speed;

        rb.velocity = new Vector2(speed, rb.velocity.y);

    }
}

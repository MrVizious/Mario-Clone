using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;

    public float maxRunningSpeed = 5.625f;
    private float horizontalInput;

    private IEnumerator distanceCoroutine = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Fire1"))
        {
            if (distanceCoroutine == null)
            {
                distanceCoroutine = DistanceCoroutine();
                StartCoroutine(distanceCoroutine);
            }
        }
    }

    private void FixedUpdate()
    {

        if (horizontalInput < 0f)
        {
            rb.velocity = new Vector2(-maxRunningSpeed, rb.velocity.y);
        }
        else if (horizontalInput > 0f)
        {
            rb.velocity = new Vector2(maxRunningSpeed, rb.velocity.y);
        }
        else rb.velocity = new Vector2(0f, rb.velocity.y);

    }

    private IEnumerator DistanceCoroutine()
    {
        float initialPositionX = transform.position.x;
        yield return new WaitForSeconds(1f);
        Debug.Log("Distance traveled in one second: " + Mathf.Abs(transform.position.x - initialPositionX));
        distanceCoroutine = null;
    }
}
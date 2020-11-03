using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;

    public float maxRunningSpeed = 5.625f;
    private float horizontalInput;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Horizontal Input: " + (float)horizontalInput);
    }

    private void FixedUpdate()
    {

        horizontalInput = Input.GetAxis("Horizontal");
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
}
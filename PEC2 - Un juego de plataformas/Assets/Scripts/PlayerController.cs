using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;

    public float maxWalkingSpeed = 5.625f;
    public float horizontalInput;

    private IEnumerator distanceCoroutine = null;

    public PlayerMovementState state;

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
            if (state == null)
            {
                state = gameObject.AddComponent<WalkingState>();
            }
        }
    }

    private void FixedUpdate()
    {



    }

    private IEnumerator DistanceCoroutine()
    {
        float initialPositionX = transform.position.x;
        yield return new WaitForSeconds(1f);
        Debug.Log("Distance traveled in one second: " + Mathf.Abs(transform.position.x - initialPositionX));
        distanceCoroutine = null;
    }
}
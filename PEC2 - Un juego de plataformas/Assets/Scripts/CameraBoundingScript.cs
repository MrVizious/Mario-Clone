using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBoundingScript : MonoBehaviour
{
    public float MIN_X_POSITION = 5f, MAX_X_POSITION = Mathf.Infinity;
    public float MIN_Y_POSITION = 4.5f, MAX_Y_POSITION = Mathf.Infinity;

    public float moveSpeed = 2f;

    public bool getMinPositionsOnStart = true;

    public enum CameraBehaviours
    {
        FollowPlayer,
        FreeMovement
    }

    public CameraBehaviours cameraBehaviour = CameraBehaviours.FreeMovement;

    private void Start()
    {
        if (getMinPositionsOnStart)
        {
            MIN_X_POSITION = transform.position.x;
            MIN_Y_POSITION = transform.position.y;

        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        switch (cameraBehaviour)
        {
            case CameraBehaviours.FreeMovement:
                transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * moveSpeed + Vector2.up * verticalInput * Time.deltaTime * moveSpeed);
                break;
            case CameraBehaviours.FollowPlayer:
                break;
        }

        if (transform.position.x < MIN_X_POSITION || transform.position.x > MAX_X_POSITION || transform.position.y < MIN_Y_POSITION || transform.position.y > MAX_Y_POSITION)
        {
            Debug.Log("Clamping!");
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, MIN_X_POSITION, MAX_X_POSITION), Mathf.Clamp(transform.position.y, MIN_Y_POSITION, MAX_Y_POSITION), transform.position.z);
        }
    }

}

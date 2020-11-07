using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBoundingScript : MonoBehaviour
{
    public Transform player;
    public float MIN_X_POSITION = 5f, MAX_X_POSITION = Mathf.Infinity;
    public float MIN_Y_POSITION = 4.5f, MAX_Y_POSITION = Mathf.Infinity;

    public float moveSpeed = 2f;

    public bool getMinConstraintsOnStart = true;

    public enum CameraBehaviours
    {
        FollowPlayer,
        FreeMovement
    }

    public CameraBehaviours cameraBehaviour = CameraBehaviours.FreeMovement;

    private void Start()
    {
        //Get minimum constraints if the option is chosen based on the initial position
        if (getMinConstraintsOnStart) SetMinimumConstraintsAsCurrentCameraPosition();
    }

    void Update()
    {

        switch (cameraBehaviour)
        {
            //If Free Camera movement is chosen
            case CameraBehaviours.FreeMovement:
                FreeMovement();
                break;

            //If Follow Player camera movement is chosen
            case CameraBehaviours.FollowPlayer:
                FollowPlayer();
                SetNewConstraints();
                break;
        }

        ClampPosition();

    }

    /// <summary>
    /// Move player according to player inputs, not player position
    /// </summary>
    private void FreeMovement()
    {
        //Get inputs from the user
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //Apply movement
        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * moveSpeed + Vector2.up * verticalInput * Time.deltaTime * moveSpeed);
    }

    private void FollowPlayer()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    private void SetNewConstraints()
    {
        if (transform.position.x > MIN_X_POSITION) MIN_X_POSITION = transform.position.x;
    }

    /// <summary>
    /// Clamps the current position of the camera depending on the established constraints
    /// </summary>
    private void ClampPosition()
    {
        if (transform.position.x < MIN_X_POSITION || transform.position.x > MAX_X_POSITION || transform.position.y < MIN_Y_POSITION || transform.position.y > MAX_Y_POSITION)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, MIN_X_POSITION, MAX_X_POSITION), Mathf.Clamp(transform.position.y, MIN_Y_POSITION, MAX_Y_POSITION), transform.position.z);
        }
    }

    /// <summary>
    /// Sets minimum Y constraint using the current position of the camera
    /// </summary>
    private void SetMinimumConstraintsAsCurrentCameraPosition()
    {
        MIN_Y_POSITION = transform.position.y;
    }
}

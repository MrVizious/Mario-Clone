using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    //Value around 0 in which the horizontal input isn't taken into account
    public static float HORIZONTAL_DEAD_ZONE = 0.05f;


    //Different input values
    public int horizontalInput = 0;
    public bool runInputHeldDown = false, jumpInputPressedDown = false, jumpInputHeldDown = false;

    void Update()
    {
        UpdateHorizontalInput();
        //UpdateRunInputPressed();
        UpdateJumpInput();
    }

    /// <summary>
    /// Update the horizontal axis input transforming it to either -1, 0 or 1
    /// </summary>
    private void UpdateHorizontalInput()
    {
        float currentHorizontalInput = Input.GetAxis("Horizontal");
        if (currentHorizontalInput > HORIZONTAL_DEAD_ZONE) horizontalInput = 1;
        else if (currentHorizontalInput < -HORIZONTAL_DEAD_ZONE) horizontalInput = -1;
        else horizontalInput = 0;
    }

    /// <summary>
    /// Updates the value that keeps track of whether the run button 
    /// is being held down
    /// </summary>
    private void UpdateRunInputPressed()
    {
        runInputHeldDown = Input.GetButtonDown("Run");
    }

    /// <summary>
    /// Updates the value that keeps track of whether the jump button was just pressed 
    /// down or is being held down
    /// </summary>
    private void UpdateJumpInput()
    {
        jumpInputPressedDown = Input.GetButtonDown("Jump");
        jumpInputHeldDown = Input.GetButton("Jump");
    }

}
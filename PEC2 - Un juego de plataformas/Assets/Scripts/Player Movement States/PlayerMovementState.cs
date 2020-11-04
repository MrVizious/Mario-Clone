using UnityEngine;

public abstract class PlayerMovementState : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected PlayerController playerController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        Debug.Log("State started: " + this.GetType());
    }
}

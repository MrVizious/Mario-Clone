using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;
    private PlayerInput input;
    private PlayerMovement movement;
    public SceneController sceneController;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        animator.SetBool("Walking", input.horizontalInput != 0f);
        animator.SetBool("Jumping", !movement.grounded);
        UpdateSpriteDirection();
    }

    /// <summary>
    /// Updates the direction of the sprite depending on the input
    /// </summary>
    private void UpdateSpriteDirection()
    {
        if (input.horizontalInput > 0f)
        {
            sprite.flipX = false;
        }
        else if (input.horizontalInput < 0f)
        {
            sprite.flipX = true;
        }
    }

    /// <summary>
    /// Stops the player in place and restarts the game after a short time
    /// </summary>
    public void Die()
    {
        GetComponent<PlayerInput>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.gravityScale = 0f;
        rb.velocity = Vector3.zero;
        animator.SetBool("Dead", true);
        StartCoroutine(DieAnimation());
    }

    /// <summary>
    /// Restarts the game after a short time
    /// </summary>
    /// <returns></returns>
    private IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        sceneController.GoToEndMenu();
    }
}
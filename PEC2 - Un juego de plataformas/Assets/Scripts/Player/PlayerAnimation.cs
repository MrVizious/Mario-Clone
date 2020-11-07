using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer sprite;
    private PlayerInput input;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInput>();
    }

    private void Update()
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

    public void Die()
    {
        GetComponent<PlayerInput>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.gravityScale = 0f;
        rb.velocity = Vector3.zero;
        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return null;
    }
}
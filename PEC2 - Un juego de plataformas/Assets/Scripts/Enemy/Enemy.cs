using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Makes the player jump and destroys this enemy
    /// </summary>
    /// <param name="playerMovement"></param>
    public void Die(PlayerMovement playerMovement)
    {
        playerMovement.Jump();
        GetComponent<Animator>().SetBool("Dead", true);
        GetComponent<GoombaMovement>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }
}

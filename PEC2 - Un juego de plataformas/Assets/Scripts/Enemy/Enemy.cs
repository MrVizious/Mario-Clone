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
        Destroy(gameObject);
    }
}

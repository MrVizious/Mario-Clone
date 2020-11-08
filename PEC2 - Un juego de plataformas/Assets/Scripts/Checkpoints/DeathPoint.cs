using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If the player touches it, they die
        if (other.tag.Equals("Player")) other.gameObject.GetComponent<PlayerAnimation>().Die();
    }
}

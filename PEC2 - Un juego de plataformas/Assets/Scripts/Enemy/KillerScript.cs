using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerScript : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerAnimation>().Die();
        }
    }

    private void Update()
    {

    }

}

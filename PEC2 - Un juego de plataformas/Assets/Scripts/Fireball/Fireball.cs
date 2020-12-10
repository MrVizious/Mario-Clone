using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int maxNumberOfBounces = 3;
    public float initialSpeed = 10f;
    private int currentNumberOfBounces = 0;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(initialSpeed * transform.right.x, -initialSpeed);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().Die(null);
            Destroy(gameObject);
        }
        currentNumberOfBounces++;
        if (currentNumberOfBounces == maxNumberOfBounces) Destroy(gameObject);
    }

}

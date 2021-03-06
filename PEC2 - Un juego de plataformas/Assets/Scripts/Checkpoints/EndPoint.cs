using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EndPoint : MonoBehaviour
{
    public LevelController level;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    /// <summary>
    /// If the player touches the end point, the player wins
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            level.FinishLevel();
        }
    }

}

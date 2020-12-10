using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class CheckPoint : MonoBehaviour
{
    private void Start()
    {
        // Makes sure the collider is a trigger
        GetComponent<Collider2D>().isTrigger = true;
    }
    /// <summary>
    /// If the player hits a checkpoint, it will try to be the newest checkpoint
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("Checkpoint!");
            GameObject.Find("LevelController").GetComponent<LevelController>().setLatestCheckPoint(transform.position);
        }
    }
}

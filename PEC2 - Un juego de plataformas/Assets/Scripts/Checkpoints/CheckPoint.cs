using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class CheckPoint : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("Checkpoint!");
            GameObject.Find("LevelController").GetComponent<LevelController>().setLatestCheckPoint(transform.position);
        }
    }
}

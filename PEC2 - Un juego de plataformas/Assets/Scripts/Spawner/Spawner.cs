using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> gameobjectsToSpawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player")) Spawn();
    }

    private void Spawn()
    {
        foreach (GameObject item in gameobjectsToSpawn)
        {
            item.SetActive(true);
        }
    }
}

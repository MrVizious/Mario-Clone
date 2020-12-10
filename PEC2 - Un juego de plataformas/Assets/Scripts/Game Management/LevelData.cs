using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Level", order = 1)]
public class LevelData : ScriptableObject
{
    public int currentNumberOfLifes = 3, maxNumberOfLifes = 3;
    private Vector2 latestCheckPoint;

    /// <summary>
    /// Sets everything as a new game
    /// </summary>
    public void Reset()
    {
        currentNumberOfLifes = maxNumberOfLifes;
        latestCheckPoint = new Vector2(0f, 0f);
    }

    /// <summary>
    /// Sets the new position as the latests checkpoint without checking it
    /// </summary>
    /// <param name="newCheckPoint"></param>
    public void setLatestCheckPoint(Vector2 newCheckPoint)
    {
        latestCheckPoint = newCheckPoint;
    }

    /// <summary>
    /// Returns the latest checkpoint
    /// </summary>
    /// <returns></returns>
    public Vector2 getLatestCheckPoint()
    {
        return latestCheckPoint;
    }

}

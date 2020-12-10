using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Level", order = 1)]
public class LevelData : ScriptableObject
{
    public int currentNumberOfLifes = 3, maxNumberOfLifes = 3;
    private Vector2 latestCheckPoint;

    public void Reset()
    {
        currentNumberOfLifes = maxNumberOfLifes;
        latestCheckPoint = new Vector2(0f, 0f);
    }

    public void setLatestCheckPoint(Vector2 newCheckPoint)
    {
        latestCheckPoint = newCheckPoint;
    }

    public Vector2 getLatestCheckPoint()
    {
        return latestCheckPoint;
    }

}

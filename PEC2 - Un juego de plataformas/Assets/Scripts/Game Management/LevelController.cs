using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public LevelData data;
    public GameObject player;
    public SceneController scene;

    /// <summary>
    /// Sets the player's position as the latest checkpoint
    /// </summary>
    private void Start()
    {
        player.transform.position = data.getLatestCheckPoint();
    }

    /// <summary>
    /// Substracts one life and then decides whether to restart from the last checkpoint or
    /// go to the end menu
    /// </summary>
    public void Die()
    {
        data.currentNumberOfLifes--;
        if (data.currentNumberOfLifes > 0)
        {
            scene.StartGame();
        }
        else
        {
            data.Reset();
            scene.GoToEndMenu();
        }
    }

    /// <summary>
    /// Checks if the new checkpoint is the furthest and sets it
    /// </summary>
    /// <param name="newCheckPoint"></param>
    public void setLatestCheckPoint(Vector2 newCheckPoint)
    {
        if (newCheckPoint.x > data.getLatestCheckPoint().x)
        {
            data.setLatestCheckPoint(newCheckPoint);
        }
    }

    /// <summary>
    /// Resets the data of the level and sends the player to the end menu
    /// </summary>
    public void FinishLevel()
    {
        data.Reset();
        scene.GoToEndMenu();
    }

}

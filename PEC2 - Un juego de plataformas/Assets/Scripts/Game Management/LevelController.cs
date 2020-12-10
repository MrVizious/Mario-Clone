using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public LevelData data;
    public GameObject player;
    public SceneController scene;

    private void Start()
    {
        player.transform.position = data.getLatestCheckPoint();
    }

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

    public void setLatestCheckPoint(Vector2 newCheckPoint)
    {
        if (newCheckPoint.x > data.getLatestCheckPoint().x)
        {
            data.setLatestCheckPoint(newCheckPoint);
        }
    }

    public void FinishLevel()
    {
        data.Reset();
        scene.GoToEndMenu();
    }

}

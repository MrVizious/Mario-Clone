using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HealthUI : MonoBehaviour
{
    public LevelData data;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    /// <summary>
    /// Updates the information shown
    /// </summary>
    private void Update()
    {
        text.text = "Lifes: " + data.currentNumberOfLifes;
    }

}

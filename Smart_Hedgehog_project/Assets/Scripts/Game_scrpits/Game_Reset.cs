using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Reset : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (GameConfig.record_score < GameConfig.score)
        {
            GameConfig.record_score = GameConfig.score;

            PlayerPrefs.SetInt("Record_score", GameConfig.record_score); //salvam pentru toate sesiunile recordul
        }

        GameConfig.score = 0;
        
    }
}

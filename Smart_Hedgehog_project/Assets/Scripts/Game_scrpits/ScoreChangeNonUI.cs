using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreChangeNonUI : MonoBehaviour
{
    private TextMeshPro txt;

    public string text_title;

    public int score_tip;

    private void Start()
    {
        txt = GetComponent<TMPro.TextMeshPro>();
    }

    void Update()
    {
        change_score();
    }

    void change_score()
    {
        switch (score_tip)
        {
            case 0: //scorul din real time
                txt.text = text_title + GameConfig.score.ToString();
                break;
            case 1: //scorul record
                if (GameConfig.record_score != 0)
                    txt.text = text_title + GameConfig.record_score.ToString();
                break;

            default:
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public float score;
    // Update is called once per frame
    void Update()
    {
        if (!Cinematic.playingCinematic) //score will go up once cinematic is over
        {
            score += 0.5f;
            scoreText.text = score.ToString("Score:0");
        }
    }
}

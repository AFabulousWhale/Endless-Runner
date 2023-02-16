using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public List<float> currentScores = new List<float>();
    int leaderboardLength = 5;

    public TextMeshProUGUI lbText;

    int placement;

    void Start()
    {
        GetScores();
    }

    public void GetScores()
    {
        currentScores[0] = PlayerPrefs.GetFloat("firstPlace");
        currentScores[1] = PlayerPrefs.GetFloat("secondPlace");
        currentScores[2] = PlayerPrefs.GetFloat("thirdPlace");
        currentScores[3] = PlayerPrefs.GetFloat("fourthPlace");
        currentScores[4] = PlayerPrefs.GetFloat("fifthPlace");
    }

    public void OnDeath()
    {
        PlayerPrefs.SetFloat("firstPlace", currentScores[0]);
        PlayerPrefs.SetFloat("secondPlace", currentScores[1]);
        PlayerPrefs.SetFloat("thirdPlace", currentScores[2]);
        PlayerPrefs.SetFloat("fourthPlace", currentScores[3]);
        PlayerPrefs.SetFloat("fifthPlace", currentScores[4]);
        PlayerPrefs.Save();
    }

    public void CompareScore(float playerScore)
    {
        Debug.Log(playerScore);
        for (int i = 0; i < leaderboardLength; i++) //repeat the number of leaderboard spots - so if it is out of 5 or 10
        {
            if(i == 0 && playerScore > currentScores[0]) //checking if the player's score is more than first place
            {
                placement = 1;
            }
            else if(playerScore < currentScores[i]) //checking the placement of the player's score by seeing if it less than each score in the list of overall scores
            {
                placement = (i + 2);
            }
        }
        if ((placement - 1) != leaderboardLength) //if the score isn't below last place then it add the new score to the list
        {
            currentScores.RemoveAt((leaderboardLength - 1));
            currentScores.Insert((placement - 1), playerScore);
            Debug.Log(placement);
        }
    }

    public void UpdateScore()
    {
        lbText.text = "";
        int index = 1;
        foreach (var item in currentScores)
        {
            lbText.text += $"{index}: {item}<br>";
            index++;
        }
    }
}
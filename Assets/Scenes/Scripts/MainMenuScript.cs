using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainCanvas, optionsCanvas, lbCanvas;

    public void StartGame()
    {
        SceneManager.LoadScene("MainGameplay");
    }

    public void Options()
    {
        optionsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void LeaderBoard()
    {
        lbCanvas.GetComponent<Leaderboard>().GetScores();
        lbCanvas.GetComponent<Leaderboard>().UpdateScore();
        lbCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        lbCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
}

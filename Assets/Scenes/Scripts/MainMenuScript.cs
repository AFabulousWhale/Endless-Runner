using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainCanvas, optionsCanvas;

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
        //go to leaderboard scene
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        optionsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
}

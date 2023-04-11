using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainCanvas, optionsCanvas, lbCanvas;
    public AudioSource buttonPress, buttonHover;

    string menu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu != "main")
            {
                Back();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGameplay");
    }

    public void Options()
    {
        menu = "options";
        optionsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void LeaderBoard()
    {
        menu = "leaderboard";
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
        menu = "main";
        lbCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void Pressed()
    {
        buttonPress.Play();
    }

    public void Hover()
    {
        buttonHover.Play();
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Game()
    {
        SceneManager.LoadScene("MainGameplay");
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

}

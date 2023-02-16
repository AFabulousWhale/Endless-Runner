using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject optionsMenu, pauseMenu, platforms;
    public Score scoreScript;
    public Shark sharkScript;

    string menu = "game";

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menu == "game")
            {
                menu = "paused";
                Pause();
            }
            else if (menu == "paused")
            {
                menu = "game";
                Resume();
            }
            else if (menu == "options")
            {
                menu = "paused";
                Pause();
            }
        }
    }

    public void Options()
    {
        menu = "options";
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        foreach (Transform item in platforms.transform)
        {
            foreach (Transform child in item)
            {
                if(child.tag == "Platform" || child.tag == "Walls")
                {
                    child.GetComponent<FloorController>().enabled = false;
                }
            }
        }
        scoreScript.enabled = false;
        sharkScript.enabled = false;
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Menu()
    {
        //go to menu scene
    }

    public void Resume()
    {
        Time.timeScale = 1;
        foreach (Transform item in platforms.transform)
        {
            foreach (Transform child in item)
            {
                if(child.tag == "Platform" || child.tag == "Walls")
                {
                    child.GetComponent<FloorController>().enabled = true;
                }
            }
        }
        scoreScript.enabled = true;
        sharkScript.enabled = true;
        pauseMenu.SetActive(false);
    }
}

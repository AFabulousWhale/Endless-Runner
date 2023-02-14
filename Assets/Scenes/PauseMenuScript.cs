using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject optionsMenu, pauseMenu;
    public AudioMixer masterMixer;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
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
        pauseMenu.SetActive(false);
    }

    public void ChangeSFXVolume(float volume)
    {
        volume = Mathf.Log(volume) * 20; //converting volume from linear to logarithmic scale
        masterMixer.SetFloat("SFXVolume", volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        volume = Mathf.Log(volume) * 20;
        masterMixer.SetFloat("MusicVolume", volume);
    }

    public void ChangeMainVolume(float volume)
    {
        volume = Mathf.Log(volume) * 20;
        masterMixer.SetFloat("MainVolume", volume);
    }


}

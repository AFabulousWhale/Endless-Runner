using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider SFXSlider, MusicSlider, MainSlider;

    float MusicVol, SFXVol, MainVol;

    private void Start()
    {
        MusicVol = PlayerPrefs.GetFloat("Music");
        SFXVol = PlayerPrefs.GetFloat("SFX");
        MainVol = PlayerPrefs.GetFloat("Main");

        //if nothing is saved then it will be max volume
        if(MusicVol != 0)
        {
            MusicSlider.value = MusicVol;
            ChangeMusicVolume(MusicVol);
        }
        if (SFXVol != 0)
        {
            SFXSlider.value = SFXVol;
            ChangeSFXVolume(SFXVol);
        }
        if (MainVol != 0)
        {
            MainSlider.value = MainVol;
            ChangeMainVolume(MainVol);
        }
    }

    public void ChangeSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFX", volume);
        volume = Mathf.Log(volume) * 20; //converting volume from linear to logarithmic scale
        masterMixer.SetFloat("SFXVolume", volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("Music", volume);
        volume = Mathf.Log(volume) * 20;
        masterMixer.SetFloat("MusicVolume", volume);
    }

    public void ChangeMainVolume(float volume)
    {
        PlayerPrefs.SetFloat("Main", volume);
        volume = Mathf.Log(volume) * 20;
        masterMixer.SetFloat("MainVolume", volume);
    }
}

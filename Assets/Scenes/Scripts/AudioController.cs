using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer masterMixer;

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

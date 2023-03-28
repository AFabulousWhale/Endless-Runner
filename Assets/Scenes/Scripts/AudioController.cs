using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioController : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider SFXSlider, MusicSlider, MainSlider;

    public TextMeshProUGUI[] smallText;
    public TextMeshProUGUI[] mediumText;
    public TextMeshProUGUI[] bigText;

    public TMP_FontAsset smallFont;
    public TMP_FontAsset mediumFont;
    public TMP_FontAsset bigFont;
    public TMP_FontAsset SansSerif;

    float MusicVol, SFXVol, MainVol;

    bool fontChanged;

    private void Start()
    {
        //fontChanged = intToBool(PlayerPrefs.GetInt("FontChange"));
        //foreach (TextMeshProUGUI text in smallText)
        //{
        //    if(fontChanged == true)
        //    {
        //        text.font = SansSerif;
        //    }
        //    else
        //    {
        //        text.font = smallFont;
        //    }
        //}

        //foreach (TextMeshProUGUI text in bigText)
        //{
        //    if (fontChanged == true)
        //    {
        //        text.font = SansSerif;
        //    }
        //    else
        //    {
        //        text.font = bigFont;
        //    }
        //}

        //foreach (TextMeshProUGUI text in mediumText)
        //{
        //    if (fontChanged == true)
        //    {
        //        text.font = SansSerif;
        //    }
        //    else
        //    {
        //        text.font = mediumFont;
        //    }
        //}
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

    public void ChangeFont()
    {
        fontChanged = !fontChanged;
        PlayerPrefs.SetInt("FontChange", boolToInt(fontChanged));

        foreach (TextMeshProUGUI text in smallText)
        {
            if (fontChanged == true)
            {
                text.font = SansSerif;
            }
            else
            {
                text.font = smallFont;
            }
        }

        foreach (TextMeshProUGUI text in bigText)
        {
            if (fontChanged == true)
            {
                text.font = SansSerif;
            }
            else
            {
                text.font = bigFont;
            }
        }

        foreach (TextMeshProUGUI text in mediumText)
        {
            if (fontChanged == true)
            {
                text.font = SansSerif;
            }
            else
            {
                text.font = mediumFont;
            }
        }
    }

    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}

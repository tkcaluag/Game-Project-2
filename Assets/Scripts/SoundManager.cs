using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] AudioMixer SFXMixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        } else
        {
            LoadMusicVol();
        }

        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1);
        } else
        {
            LoadSFXVol();
        }
    }

    public void changeMusicVolume()
    {
        float musicVolume = musicSlider.value;
        musicMixer.SetFloat("Music", Mathf.Log10(musicVolume)*20);
        SaveMusicVol();
    }

    public void changeSFXVolume()
    {
        float SFXVolume = SFXSlider.value;
        SFXMixer.SetFloat("SFX", MathF.Log10(SFXVolume)*20);
        SaveSFXVol();
    }

    private void LoadMusicVol()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        musicMixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume"))*20);
    }

    private void SaveMusicVol()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    private void LoadSFXVol()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SFXMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume"))*20);
    }

    private void SaveSFXVol()
    {
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }

}

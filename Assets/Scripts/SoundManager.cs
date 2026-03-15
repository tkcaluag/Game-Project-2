using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixer mixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        } else
        {
            Load();
        }
    }

    public void changeVolume()
    {
        float musicVolume = volumeSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(musicVolume)*20);
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}

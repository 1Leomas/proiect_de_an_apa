using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class game_pause : MonoBehaviour
{
    bool pause = false;
    public GameObject menu;

    public AudioMixerGroup Mixer;

    public AudioMixerSnapshot Normal;
    public AudioMixerSnapshot In_menu;

    public Toggle toggle_music;
    public Slider slider_volume;

    private void Start()
    {
        toggle_music.isOn = PlayerPrefs.GetInt("MusicEnabled", 0) == 1;
        slider_volume.value = PlayerPrefs.GetFloat("MasterVolume", 1);
    }

    public void PauseGame()
    {
        if (!pause)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            pause = true;

            In_menu.TransitionTo(.5f);
        }
        else
        {
            Time.timeScale = 1;
            menu.SetActive(false);
            pause = false;

            Normal.TransitionTo(2.5f);
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            pause = true;
        }
    }

    public void ToggleMusic(bool enabled)
    {
        if (enabled == true)
            Mixer.audioMixer.SetFloat("MusicVolume", -80);
        else if(enabled == false)
            Mixer.audioMixer.SetFloat("MusicVolume", 0);

        PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
    }

    public void ChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80,0,volume));

        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}

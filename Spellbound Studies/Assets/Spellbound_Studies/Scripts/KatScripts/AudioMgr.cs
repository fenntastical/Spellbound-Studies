using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class AudioMgr : MonoBehaviour
{
    public static AudioMgr Instance;
    public Sound[] MusicSounds, SFXSounds;
    public AudioSource MusicSource, SFXSource;
    public string AudioName;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PlayMusic(AudioName);
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(MusicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            MusicSource.clip = s.clip;
            MusicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(SFXSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            SFXSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        MusicSource.mute = !MusicSource.mute;
    }
    public void ToggleSFX()
    {
        SFXSource.mute = !SFXSource.mute;
    }

    public void MusicVolume(float volume)
    {
        MusicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        SFXSource.volume = volume;
    }
}

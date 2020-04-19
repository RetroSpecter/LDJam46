using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// To use this, Just do
/// AudioManager.instance.Play("SOund name");
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = masterVolume * s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in hurtSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = masterVolume * s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in recoverySounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = masterVolume * s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    #endregion

    public float masterVolume = 1;
    public Sound[] sounds;
    public Sound[] hurtSounds;
    public Sound[] recoverySounds;

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Play();
    }

    public void PlayRandomHurt()
    {
        Sound s = hurtSounds[UnityEngine.Random.Range(0, hurtSounds.Length)];
        if (s != null)
        {
            s.source.Play();
        }
    }

    public void PlayRandomRecovery()
    {
        Sound s = recoverySounds[UnityEngine.Random.Range(0, recoverySounds.Length)];
        if (s != null)
        {
            s.source.Play();
        }
    }

    public void ChangeSoundVolume(string name, float newVolume) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.volume = masterVolume * newVolume;
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Stop();
    }

    public bool IsPlaying(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            return s.source.isPlaying;
        return false;
    }

    public void StopAllSounds() {
        foreach (Sound s in sounds) {
            s.source.Stop();
        }
    }
}

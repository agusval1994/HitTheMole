using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    int music;
    int fx;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    private void Start()
    {
        LoadAudioData();
    }

    public void LoadAudioData()
    {
        music = PlayerPrefs.GetInt("Music");
        fx = PlayerPrefs.GetInt("Fx");
    }

    public void Play(string name, string soundType)
    {
        if(music == 0 && soundType == "Music")
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            s.source.Play();
        }

        if (fx == 0 && soundType == "Fx")
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void UpdateMusic()
    {
        music = PlayerPrefs.GetInt("Music");

        if(music == 0)
        {
            Play("MainTheme", "Music");
        }
        else
        {
            Stop("MainTheme");
        }
    }

    public void UpdateFx()
    {
        fx = PlayerPrefs.GetInt("Fx");
    }
}

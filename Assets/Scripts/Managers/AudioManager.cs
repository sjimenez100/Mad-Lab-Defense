using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager main;

    public Sound[] sounds;


    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (main is null)
            main = this;
        else
            Destroy(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
        }

    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source?.Play();
    }

}

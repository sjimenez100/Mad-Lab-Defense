using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager main;

    public Sound[] sounds;


    protected virtual void Awake()
    {
        Singleton();
        CreateAudioSources();
        
    }

    private void Singleton()
    {
        if (this is not RetainedAudioManager)
        {
            if (main is null)
                main = this;
            else
                Destroy(this);

            Debug.Log(main.name);
        }

    }

    private void CreateAudioSources()
    {
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

        if (sound != null)
            sound.source.Play();
        else
            Debug.LogWarning($"Sound: {sound.name} registered as null");
    }

    private void OnDestroy()
    {
        if(this == main)
            main = null;
    }

}

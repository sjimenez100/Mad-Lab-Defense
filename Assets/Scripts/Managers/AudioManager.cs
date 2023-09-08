using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager main;

    [SerializeField]
    private SoundProfile profile;

    protected virtual void Awake()
    {
        Singleton();
        CreateAudioSources();
    }

    private void Singleton()
    {
        if (this is not RetainedAudioManager)
        {
            if (main == null)
                main = this;
            else
                Destroy(gameObject);
        }

    }

    private void CreateAudioSources()
    {
        foreach (Sound sound in profile.sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.enabled = false;
            sound.source.outputAudioMixerGroup = sound.outputGroup;
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake= sound.playOnAwake;
            sound.source.enabled = true;
        }
    }


    public void PlaySound(string name)
    {
        Sound sound = Array.Find(profile.sounds, sound => sound.name == name);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundController : Singleton<SoundController>
{
    [SerializeField]
    private Sound[] _Sounds;

    protected override void Awake()
    {
        base.Awake();
        foreach (Sound sound in _Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(_Sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.LogError($"SoundClip {sound.name} not found");
            return;
        }
        sound.source.Play();
    }
}

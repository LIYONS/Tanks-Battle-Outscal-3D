using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingletonGeneric<AudioManager>
{
    [SerializeField] private AudioSource musicAS;
    [SerializeField] private AudioSource sfxAS;
    [SerializeField] private AudioSource gameAS;
    [SerializeField] private float musicVolume;
    [SerializeField] private float sfxVolume;
    [SerializeField] private float gameVolume;
    [SerializeField] private List<Sounds> sounds;

    private void Start()
    {
        musicAS.volume = musicVolume;
        sfxAS.volume = sfxVolume;
        gameAS.volume = gameVolume;
        PlayMusic(SoundType.BackGroundMusic);
    }

    public void PlayMusic(SoundType soundType)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            musicAS.clip = clip;
            musicAS.Play();
        }
    }
    public void PlayGameSound(SoundType soundType)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            gameAS.clip = clip;
            gameAS.Play();
        }
    }
    public void PlaySfx(SoundType soundType)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            sfxAS.PlayOneShot(clip);
        }
    }
    private AudioClip GetSoundClip(SoundType soundType)
    {
        return sounds.Find(clip => clip.soundType == soundType).audioClip;
    }

    public void Mute()
    {
        musicAS.volume = 0f;
        sfxAS.volume = 0f;
        gameAS.volume = 0f;
    }
    public void StopMusic()
    {
        musicAS.volume = 0f;
    }

    public void ResetSounds()
    {
        musicAS.volume = musicVolume;
        sfxAS.volume = sfxVolume;
        gameAS.volume = gameVolume;
    }
}

[Serializable]
public class Sounds
{
    public SoundType soundType;
    public AudioClip audioClip;
}
public enum SoundType
{
    BackGroundMusic,
    TankIdle,
    ButtonClick,
    Fire,
    ShellExplode,
    TankExplode,
    Achievement
}

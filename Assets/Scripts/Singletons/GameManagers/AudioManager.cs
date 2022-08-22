using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingletonGeneric<AudioManager>
{
    [SerializeField] private AudioSource musicAS;
    [SerializeField] private AudioSource sfxAS;
    [SerializeField] private AudioSource gameAS;
    [SerializeField] private List<Sounds> sounds;

    private const string musicVolume = "musicVolume";
    private const string gameVolume = "gameVolume";
    private const string sfxVolume = "sfxVolume";

    private void Start()
    {
        ResetSounds();
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
        if(PlayerPrefs.HasKey(musicVolume))
        {
            musicAS.volume = PlayerPrefs.GetFloat(musicVolume);
        }
        if(PlayerPrefs.HasKey(sfxVolume))
        {
            sfxAS.volume = PlayerPrefs.GetFloat(sfxVolume);
        }
        if(PlayerPrefs.HasKey(gameVolume))
        {
            gameAS.volume = PlayerPrefs.GetFloat(gameVolume);
        } 
    }

    public void SetMusicVolume(float _volume)
    {
        musicAS.volume = _volume;
    }
    public void SetGameVolume(float _volume)
    {
        gameAS.volume = _volume;
    }
    public void SetSfxVolume(float _volume)
    {
        sfxAS.volume = _volume;
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

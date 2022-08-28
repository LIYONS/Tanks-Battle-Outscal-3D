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

    private const string MUSIC_VOLUME = "musicVolume";
    private const string GAME_VOLUME = "gameVolume";
    private const string SFX_VOLUME = "sfxVolume";

    private void Start()
    {
        ResetSounds();
        PlaySound(SoundType.BackGroundMusic);
    }
    public void PlaySound(SoundType soundType)
    {
        Sounds sound = GetSound(soundType);
        if (sound.audioClip)
        {
            switch (sound.audioSourceType)
            {
                case AudioSourceType.Sfx:
                    {
                        sfxAS.PlayOneShot(sound.audioClip);
                        break;
                    }      
                case AudioSourceType.Music:
                    {
                        musicAS.clip = sound.audioClip;
                        musicAS.Play();
                        break;
                    }
                case AudioSourceType.Game:
                    {
                        gameAS.clip = sound.audioClip;
                        gameAS.Play();
                        break;
                    }
            }
        }
    }
    private Sounds GetSound(SoundType soundType)
    {
        return sounds.Find(clip => clip.soundType == soundType);
    }

    public void Mute()
    {
        musicAS.volume = 0f;
        sfxAS.volume = 0f;
        gameAS.volume = 0f;
    }

    public void StopAudio(AudioSourceType audioType)
    {
        switch (audioType)
        {
            case AudioSourceType.Music:
                {
                    musicAS.clip = null;
                    musicAS.volume = 0f;
                    break;
                }
            case AudioSourceType.Game:
                {
                    gameAS.clip = null;
                    gameAS.volume = 0f;
                    break;
                }
        }
    }

    public void ResetSounds()
    {
        if(PlayerPrefs.HasKey(MUSIC_VOLUME))
        {
            musicAS.volume = PlayerPrefs.GetFloat(MUSIC_VOLUME);
        }
        if(PlayerPrefs.HasKey(SFX_VOLUME))
        {
            sfxAS.volume = PlayerPrefs.GetFloat(SFX_VOLUME);
        }
        if(PlayerPrefs.HasKey(GAME_VOLUME))
        {
            gameAS.volume = PlayerPrefs.GetFloat(GAME_VOLUME);
        } 
    }
    public void SetVolume(AudioSourceType audioSourceType,float _volume)
    {
        switch (audioSourceType)
        {
            case AudioSourceType.Sfx:
                {
                    sfxAS.volume = _volume;
                    break;
                }
            case AudioSourceType.Music:
                {
                    musicAS.volume = _volume;
                    break;
                }
            case AudioSourceType.Game:
                {
                    gameAS.volume = _volume;
                    break;
                }
        }
    }
}

[Serializable]
public struct Sounds
{
    public SoundType soundType;
    public AudioSourceType audioSourceType;
    public AudioClip audioClip;
}
public enum AudioSourceType
{
    Sfx,
    Music,
    Game
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuUiHandler : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private int firstLevel;

    //Options
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider gameSlider;
    [SerializeField] private Slider sfxSlider;
    private const string musicVolume = "musicVolume";
    private const string gameVolume = "gameVolume";
    private const string sfxVolume = "sfxVolume";

    private GameManager gameManager;
    private AudioManager audioManager;
    private void Start()
    {
        optionsMenu.SetActive(false);
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        SetSliderValues();
    }

    public void StartGame()
    {
        if(gameManager)
        {
            gameManager.LoadLevel(firstLevel);
        }
    }
    public void OnButtonPress()
    {
        if(audioManager)
        {
            audioManager.PlaySfx(SoundType.ButtonClick);
        }
    }

    public void OnOptionButtonPress()
    {
        optionsMenu.SetActive(true);
    }

    public void OnOptionsClose()
    {
        optionsMenu.SetActive(false);
    }
    public void Quit()
    {
        if(gameManager)
        {
            gameManager.Quit();
        }
    }

    //OptionsMenu
    public void SetSliderValues()
    {
        if (PlayerPrefs.HasKey(musicVolume))
        {
            musicSlider.value = PlayerPrefs.GetFloat(musicVolume);
        }
        if (PlayerPrefs.HasKey(sfxVolume))
        {
            sfxSlider.value = PlayerPrefs.GetFloat(sfxVolume);
        }
        if (PlayerPrefs.HasKey(gameVolume))
        {
            gameSlider.value = PlayerPrefs.GetFloat(gameVolume);
        }
    }
    public void OnMusicSliderChanaged()
    {
        if (audioManager)
        {
            audioManager.SetMusicVolume(musicSlider.value);
            PlayerPrefs.SetFloat(musicVolume, musicSlider.value);
        }
    }

    public void OnGameSliderChanged()
    {
        if (audioManager)
        {
            audioManager.SetGameVolume(gameSlider.value);
            PlayerPrefs.SetFloat(gameVolume,gameSlider.value);
        }
    }

    public void OnSfxSliderChanged()
    {
        if (audioManager)
        {
            audioManager.SetSfxVolume(sfxSlider.value);
            PlayerPrefs.SetFloat(sfxVolume, sfxSlider.value);
        }
    }
}

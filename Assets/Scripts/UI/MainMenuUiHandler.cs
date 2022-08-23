using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUiHandler : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private int firstLevel;
    [SerializeField] private TextMeshProUGUI highScoreText;
    //Options
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider gameSlider;
    [SerializeField] private Slider sfxSlider;
    private const string musicVolume = "musicVolume";
    private const string gameVolume = "gameVolume";
    private const string sfxVolume = "sfxVolume";
    private const string highScore = "highScore";

    private GameManager gameManager;
    private AudioManager audioManager;
    private void Start()
    {
        optionsMenu.SetActive(false);
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        SetSliderValues();
        SetHighScore();
    }

    public void SetHighScore()
    {
        if(PlayerPrefs.HasKey(highScore))
        {
            highScoreText.text = "HIGHSCORE  : " + PlayerPrefs.GetInt(highScore);
            return;
        }
        highScoreText.text = "HIGHSCORE  : " + 0;
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
            PlayerPrefs.Save();
        }
    }

    public void OnGameSliderChanged()
    {
        if (audioManager)
        {
            audioManager.SetGameVolume(gameSlider.value);
            PlayerPrefs.SetFloat(gameVolume,gameSlider.value);
            PlayerPrefs.Save();
        }
    }

    public void OnSfxSliderChanged()
    {
        if (audioManager)
        {
            audioManager.SetSfxVolume(sfxSlider.value);
            PlayerPrefs.SetFloat(sfxVolume, sfxSlider.value);
            PlayerPrefs.Save();
        }
    }
}

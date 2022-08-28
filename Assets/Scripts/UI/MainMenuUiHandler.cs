using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUiHandler : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private int firstLevel;
    [SerializeField] private TextMeshProUGUI highScoreText;
    //Options
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider gameSlider;
    [SerializeField] private Slider sfxSlider;
    private const string MUSIC_VOLUME = "musicVolume";
    private const string GAME_VOLUME = "gameVolume";
    private const string SFX_VOLUME = "sfxVolume";
    private const string HIGH_SCORE = "highScore";

    private SceneManager gameManager;
    private AudioManager audioManager;
    private void Start()
    {
        optionsMenu.SetActive(false);
        mainMenuPanel.SetActive(true);
        gameManager = SceneManager.Instance;
        audioManager = AudioManager.Instance;
        SetSliderValues();
        SetHighScore();
    }

    public void SetHighScore()
    {
        if(PlayerPrefs.HasKey(HIGH_SCORE))
        {
            highScoreText.text = "HIGHSCORE  : " + PlayerPrefs.GetInt(HIGH_SCORE);
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
            audioManager.PlaySound(SoundType.ButtonClick);
        }
    }

    public void OnOptionButtonPress()
    {
        mainMenuPanel.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OnOptionsClose()
    {
        mainMenuPanel.SetActive(true);
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
        if (PlayerPrefs.HasKey(MUSIC_VOLUME))
        {
            musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME);
        }
        if (PlayerPrefs.HasKey(SFX_VOLUME))
        {
            sfxSlider.value = PlayerPrefs.GetFloat(SFX_VOLUME);
        }
        if (PlayerPrefs.HasKey(GAME_VOLUME))
        {
            gameSlider.value = PlayerPrefs.GetFloat(GAME_VOLUME);
        }
    }
    public void OnMusicSliderChanaged()
    {
        if (audioManager)
        {
            audioManager.SetVolume(AudioSourceType.Music, musicSlider.value);
            PlayerPrefs.SetFloat(MUSIC_VOLUME, musicSlider.value);
            PlayerPrefs.Save();
        }
    }

    public void OnGameSliderChanged()
    {
        if (audioManager)
        {
            audioManager.SetVolume(AudioSourceType.Game, gameSlider.value);
            PlayerPrefs.SetFloat(GAME_VOLUME,gameSlider.value);
            PlayerPrefs.Save();
        }
    }

    public void OnSfxSliderChanged()
    {
        if (audioManager)
        {
            audioManager.SetVolume(AudioSourceType.Sfx,sfxSlider.value);
            PlayerPrefs.SetFloat(SFX_VOLUME, sfxSlider.value);
            PlayerPrefs.Save();
        }
    }
}

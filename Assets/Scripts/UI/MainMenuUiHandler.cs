using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUiHandler : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private int firstLevel;

    private GameManager gameManager;
    private void Start()
    {
        optionsMenu.SetActive(false);
        gameManager = GameManager.Instance;
    }

    public void StartGame()
    {
        if(gameManager)
        {
            gameManager.LoadLevel(firstLevel);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingletonGeneric<GameManager>
{
    [SerializeField] private int mainMenuIndex;
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

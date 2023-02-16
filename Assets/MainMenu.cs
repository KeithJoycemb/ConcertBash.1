using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string gameScene;
    public GameObject options;
    public GameObject mainMenu;

    public void Start()
    {
        gameScene = "ConcertBash";
      
    }
    public void startGame()
    {
        SceneManager.LoadScene(gameScene);
    }
    public void Options()
    {
        options.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void CloseOptions()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

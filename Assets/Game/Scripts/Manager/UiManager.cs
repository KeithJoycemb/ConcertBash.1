using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    private void Start()
    {
        playButton = GetComponent<Button>();
        playButton.onClick.AddListener(startGame);
        quitButton = GetComponent<Button>();
        quitButton.onClick.AddListener(QuitGame);
    }
    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    public void PauseGame()
    {
        SceneManager.LoadScene("PauseScreen",LoadSceneMode.Additive);
    }
    public void QuitGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

}

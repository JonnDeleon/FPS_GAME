using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void MainMenuLoad()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

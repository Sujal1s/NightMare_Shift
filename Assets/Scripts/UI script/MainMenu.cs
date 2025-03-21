using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{
    public static string previousMenu = "MainMenu";

    public static void LoadSceneAndRemember(string sceneName)
    {
        previousMenu = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousMenu);
    }
}

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("level_1");
    }

    public void OpenSettings()
    {
        PlayerPrefs.SetString("PreviousScene", "MainMenu");
        PlayerPrefs.Save();

        SceneManager.LoadScene("SettingMenu");
    }


    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
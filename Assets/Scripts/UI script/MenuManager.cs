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

using UnityEngine;
using UnityEngine.SceneManagement;

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

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        if (SceneManager.GetSceneByName("PauseMenu").isLoaded)
            SceneManager.UnloadSceneAsync("PauseMenu");
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;

        if (!SceneManager.GetSceneByName("PauseMenu").isLoaded)
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
    }

    public void OpenSettings()
    {
        PlayerPrefs.SetString("PreviousScene", "PauseMenu");
        PlayerPrefs.Save();

        if (!SceneManager.GetSceneByName("SettingMenu").isLoaded)
            SceneManager.LoadScene("SettingMenu", LoadSceneMode.Additive);

        if (SceneManager.GetSceneByName("PauseMenu").isLoaded)
            SceneManager.UnloadSceneAsync("PauseMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

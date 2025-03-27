using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private string pauseSceneName = "PauseMenu";
    private string previousScene;

    public GameObject firstPauseMenuButton; // Assign in Inspector (e.g., "Resume" button)

    private void Start()
    {
        previousScene = PlayerPrefs.GetString("PreviousScene", "Level1");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start") || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        if (SceneManager.GetSceneByName(pauseSceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(pauseSceneName);
        }

        // Resume game time
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Clear UI selection to prevent conflicts
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Pause()
    {
        Debug.Log("Loading PauseMenu Scene...");

        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Stop game time
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Load Pause Menu as an overlay
        SceneManager.LoadScene(pauseSceneName, LoadSceneMode.Additive);

        // Ensure UI input is focused on the menu


        StartCoroutine(SetPauseMenuFocus());
    }

    private IEnumerator SetPauseMenuFocus()
    {
        yield return new WaitForSecondsRealtime(0.1f); // Short delay to ensure UI is ready
        EventSystem.current.SetSelectedGameObject(null); // Clear selection
        EventSystem.current.SetSelectedGameObject(firstPauseMenuButton); // Set first button
    }

    public void OpenSettings()
    {
        PlayerPrefs.SetString("PreviousScene", "PauseMenu");
        PlayerPrefs.Save();

        SceneManager.LoadScene("SettingMenu");
    }

    public void BackFromSettings()
    {
        string lastScene = PlayerPrefs.GetString("PreviousScene", "MainMenu");

        if (lastScene == "PauseMenu")
        {
            SceneManager.UnloadSceneAsync("SettingMenu");
            StartCoroutine(SetPauseMenuFocus()); // Fix controller navigation
        }
        else
        {
            SceneManager.LoadScene(lastScene);
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}

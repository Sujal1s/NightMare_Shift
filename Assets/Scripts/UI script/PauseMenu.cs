using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private string pauseSceneName = "PauseMenu"; // Ensure this matches the actual scene name
    private string previousScene;

    public GameObject firstPauseMenuButton; // Assign in the Inspector (e.g., "Resume" button)

    private void Start()
    {
        // Load the previous scene from PlayerPrefs (default to Level1 if not set)
        previousScene = PlayerPrefs.GetString("PreviousScene", "Level1");
    }

    private void Update()
    {
        // Detect "Escape" (Keyboard) OR "Select" (Gamepad) to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Select") || Input.GetKeyDown(KeyCode.JoystickButton7))
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
        else
        {
            Debug.LogWarning("PauseMenu scene is not loaded, cannot unload!");
        }

        Time.timeScale = 1f;
        GameIsPaused = false;

        // Clear UI selection to prevent focus issues
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Pause()
    {
        Debug.Log("Loading PauseMenu Scene...");

        // Save the current scene before pausing
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Load PauseMenu scene as additive (so the game scene stays loaded)
        SceneManager.LoadScene(pauseSceneName, LoadSceneMode.Additive);

        // Ensure gamepad UI navigation works by setting the selected UI element
        StartCoroutine(SetPauseMenuFocus());

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    private IEnumerator SetPauseMenuFocus()
    {
        yield return new WaitForSecondsRealtime(0.1f); // Small delay ensures UI is ready
        EventSystem.current.SetSelectedGameObject(firstPauseMenuButton);
    }

    public void OpenSettings()
    {
        // Save the current scene before opening settings
        PlayerPrefs.SetString("PreviousScene", "PauseMenu");
        PlayerPrefs.Save();

        SceneManager.LoadScene("SettingMenu");
    }

    public void BackFromSettings()
    {
        string lastScene = PlayerPrefs.GetString("PreviousScene", "MainMenu");

        if (lastScene == "PauseMenu")
        {
            Pause(); // Go back to Pause Menu
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

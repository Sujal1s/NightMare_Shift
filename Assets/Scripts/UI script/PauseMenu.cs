using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] private GameObject pauseMenuUI; // Assign in Inspector

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        else
        {
            Debug.LogError("PauseMenuUI is not assigned in the Inspector!");
        }
    }

    public void Pause()
    {
        /*        if (pauseMenuUI != null)
                {
                    pauseMenuUI.SetActive(true);
                    Time.timeScale = 0f;
                    GameIsPaused = true;
                }
                else
                {
                    Debug.LogError("PauseMenuUI is not assigned in the Inspector!");

                }*/
        SceneManager.LoadScene("PauseMenu");

    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}

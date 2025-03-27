using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Slider loadingBar;   // Assign in the Inspector
    public Text loadingText;    // Assign in the Inspector
    public float blinkSpeed = 0.5f; // Blinking speed

    void Start()
    {
        StartCoroutine(LoadNextScene());
        StartCoroutine(BlinkText()); // Make text blink
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f); // Small delay before loading starts

        AsyncOperation operation = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("NextScene"));
        operation.allowSceneActivation = false; // Prevent instant switching

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // Normalize progress
            loadingBar.value = progress; // Update UI

            if (operation.progress >= 0.9f)
            {
                loadingText.text = "Press B to Continue";
                if (Input.GetKeyDown(KeyCode.JoystickButton1)) // "B" on controller
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            loadingText.enabled = !loadingText.enabled; // Toggle text visibility
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}

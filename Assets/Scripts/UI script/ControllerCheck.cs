using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControllerCheck : MonoBehaviour
{
    public Text statusText; // Assign in Inspector
    private bool controllerConnected = false;
    public float blinkSpeed = 0.05f; // Adjust for smoother blinking

    void Start()
    {
        CheckForController();
        StartCoroutine(BlinkTextEffect());
    }

    void Update()
    {
        CheckForController();

        // If controller is connected, wait for 'B' to continue
        if (controllerConnected && Input.GetKeyDown(KeyCode.JoystickButton1)) // 'B' button
        {
            SceneManager.LoadScene("MainMenu"); // Load Main Menu
        }
    }

    void CheckForController()
    {
        if (Input.GetJoystickNames().Length > 0 && !string.IsNullOrEmpty(Input.GetJoystickNames()[0]))
        {
            statusText.text = "Controller connected Press 'B' To Continue";
            statusText.alignment = TextAnchor.MiddleCenter;
            controllerConnected = true;
        }
        else
        {
            statusText.text = "Please Connect Controller To Continue ";
            controllerConnected = false;
        }
    }

    IEnumerator BlinkTextEffect()
    {
        while (true) // Infinite loop to keep blinking
        {
            // Fade Out (smoother)
            for (float i = 1; i >= 0; i -= blinkSpeed)
            {
                statusText.color = new Color(statusText.color.r, statusText.color.g, statusText.color.b, i);
                yield return new WaitForSeconds(0.05f); // Faster transition
            }

            // Fade In (smoother)
            for (float i = 0; i <= 1; i += blinkSpeed)
            {
                statusText.color = new Color(statusText.color.r, statusText.color.g, statusText.color.b, i);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}

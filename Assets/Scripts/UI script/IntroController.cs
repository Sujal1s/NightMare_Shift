using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public Text gameTitleText;
    public Text developerText;
    public Text artText;

    public float zoomDuration = 2f;
    public float stayDuration = 1f;
    public float fadeDuration = 2f;
    public Vector3 startScale = new Vector3(0.5f, 0.5f, 1);
    public Vector3 finalScale = new Vector3(1.2f, 1.2f, 1);

    private string nextSceneName = "Controller";  // Changed to load Controller scene

    void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    IEnumerator PlayIntroSequence()
    {
        // Check if text objects are assigned
        if (gameTitleText != null)
            yield return StartCoroutine(ZoomInAndDisappear(gameTitleText, true));

        if (developerText != null)
            yield return StartCoroutine(ZoomInAndDisappear(developerText, false));

        if (artText != null)
            yield return StartCoroutine(ZoomInAndDisappear(artText, false));

        // Load the Controller Scene after the intro ends
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator ZoomInAndDisappear(Text text, bool shouldZoom)
    {
        if (text == null) yield break;  // Prevent errors if text is missing

        text.gameObject.SetActive(true);
        Color color = text.color;
        color.a = 0;
        text.color = color;

        if (shouldZoom)
        {
            text.transform.localScale = startScale;
        }

        float elapsedTime = 0f;

        // Zoom and fade-in effect
        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / zoomDuration;

            if (shouldZoom)
            {
                text.transform.localScale = Vector3.Lerp(startScale, finalScale, progress);
            }

            color.a = Mathf.Lerp(0, 1, progress);
            text.color = color;

            yield return null;
        }

        text.transform.localScale = shouldZoom ? finalScale : text.transform.localScale;
        text.color = new Color(color.r, color.g, color.b, 1);

        yield return new WaitForSeconds(stayDuration);

        elapsedTime = 0f;

        // Fade-out effect
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeDuration;

            color.a = Mathf.Lerp(1, 0, progress);
            text.color = color;

            yield return null;
        }

        text.gameObject.SetActive(false);
    }
}

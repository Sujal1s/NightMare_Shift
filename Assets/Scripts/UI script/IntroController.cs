using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public Image logoImage; 
    public Text developerText;
    public Text artText;

    public float zoomDuration = 2f;
    public float stayDuration = 1f;
    public float fadeDuration = 2f;
    public Vector3 startScale = new Vector3(0.5f, 0.5f, 1);
    public Vector3 finalScale = new Vector3(1.2f, 1.2f, 1);

    void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    IEnumerator PlayIntroSequence()
    {
        
        if (logoImage != null)
            yield return StartCoroutine(ZoomInAndDisappearImage(logoImage));

        if (developerText != null)
            yield return StartCoroutine(ZoomInAndDisappear(developerText, false));

        if (artText != null)
            yield return StartCoroutine(ZoomInAndDisappear(artText, false));

      
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    IEnumerator ZoomInAndDisappear(Text text, bool shouldZoom)
    {
        if (text == null) yield break;

        text.gameObject.SetActive(true);
        Color color = text.color;
        color.a = 0;
        text.color = color;

        if (shouldZoom)
        {
            text.transform.localScale = startScale;
        }

        float elapsedTime = 0f;

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

    IEnumerator ZoomInAndDisappearImage(Image image)
    {
        if (image == null) yield break;

        image.gameObject.SetActive(true);
        Color color = image.color;
        color.a = 0;
        image.color = color;
        image.transform.localScale = startScale;

        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / zoomDuration;

            image.transform.localScale = Vector3.Lerp(startScale, finalScale, progress);
            color.a = Mathf.Lerp(0, 1, progress);
            image.color = color;

            yield return null;
        }

        image.transform.localScale = finalScale;
        image.color = new Color(color.r, color.g, color.b, 1);

        yield return new WaitForSeconds(stayDuration);

        elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeDuration;

            color.a = Mathf.Lerp(1, 0, progress);
            image.color = color;

            yield return null;
        }

        image.gameObject.SetActive(false);
    }
}

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
    public string nextSceneName = "MainMenu";  

    void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    IEnumerator PlayIntroSequence()
    {
        
        yield return StartCoroutine(ZoomInAndDisappear(gameTitleText, true));

        
        yield return StartCoroutine(ZoomInAndDisappear(developerText, false));

     
        yield return StartCoroutine(ZoomInAndDisappear(artText, false));

        
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator ZoomInAndDisappear(Text text, bool shouldZoom)
    {
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
}

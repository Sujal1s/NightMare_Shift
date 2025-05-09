using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TMPEndCredits : MonoBehaviour
{
    public RectTransform creditsText;
    public TextMeshProUGUI tmpText;
    public TextMeshProUGUI skipText;
    public float scrollSpeed = 30f;
    public string mainMenuSceneName = "MainMenu";
    public float skipTextDelay = 3f;
    public float fadeDuration = 1.5f;
    public string text;

    private float endY;
    private bool isEnding = false;
    private float skipTextTimer = 0f;
    private bool skipFadeInComplete = false;
    

    void Start()
    {
        tmpText.text = text;


        if (skipText != null)
        {
            Color color = skipText.color;
            color.a = 0f;
            skipText.color = color;
        }

        endY = creditsText.sizeDelta.y + 800f;
    }

    void Update()
    {
        if (isEnding) return;


        creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Return))
        {
            isEnding = true;
            LoadMainMenu();
        }

        if (creditsText.anchoredPosition.y >= endY)
        {
            isEnding = true;
            LoadMainMenu();
        }


        if (skipText != null)
        {
            skipTextTimer += Time.deltaTime;

            if (!skipFadeInComplete)
            {
                if (skipTextTimer > skipTextDelay)
                {
                    float t = (skipTextTimer - skipTextDelay) / fadeDuration;
                    t = Mathf.Clamp01(t);
                    Color color = skipText.color;
                    color.a = t;
                    skipText.color = color;

                    if (t >= 1f)
                        skipFadeInComplete = true;
                }
            }
            else
            {

                float alpha = 0.5f + 0.5f * Mathf.Sin(Time.time * 2.5f);
                Color blinkColor = skipText.color;
                blinkColor.a = alpha;
                skipText.color = blinkColor;
            }
        }
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
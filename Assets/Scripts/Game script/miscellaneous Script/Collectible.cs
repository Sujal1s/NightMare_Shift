using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Sprite smallSprite; // Sprite for small scale
    public Sprite largeSprite; // Sprite for large scale
    public Vector3 largeScale = new Vector3(2f, 2f, 2f); // Scale when collected
    private Vector3 originalScale; // Original scale
    private SpriteRenderer spriteRenderer;
    public float scaleDuration = 0.5f; // Duration of the scaling animation
    public float destroyDelay = 2f; // Delay before destroying the object

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Check if the backspace key is pressed to revert the scale and sprite
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            StartCoroutine(RevertScaleAndDestroy());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Collect());
        }
    }

    IEnumerator Collect()
    {
        float elapsedTime = 0f;
        Vector3 startingScale = transform.localScale;
        spriteRenderer.sprite = largeSprite; // Change sprite to large sprite
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(startingScale, largeScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = largeScale;
    }

    IEnumerator RevertScaleAndDestroy()
    {
        float elapsedTime = 0f;
        Vector3 startingScale = transform.localScale;
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(startingScale, originalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
        spriteRenderer.sprite = smallSprite; // Change sprite to small sprite

        // Wait for the destroy delay before destroying the object
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
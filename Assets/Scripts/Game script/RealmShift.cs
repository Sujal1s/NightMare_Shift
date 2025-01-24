using UnityEngine;

public class RealmShift : MonoBehaviour
{
    public Sprite normalSprite;      // The sprite for the normal state
    public Sprite realmShiftSprite;  // The sprite for the realm shift state
    private SpriteRenderer spriteRenderer;
    private bool isRealmShifted = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))  // Press 'R' to trigger realm shift
        {
            ToggleRealmShift();
        }
    }

    void ToggleRealmShift()
    {
        if (isRealmShifted)
        {
            spriteRenderer.sprite = normalSprite;
            isRealmShifted = false;
        }
        else
        {
            spriteRenderer.sprite = realmShiftSprite;
            isRealmShifted = true;
        }
    }
}
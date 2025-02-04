using UnityEngine;

public class RealmShift : MonoBehaviour
{
    public Sprite normalSprite;      
    public Sprite realmShiftSprite;  
    private SpriteRenderer spriteRenderer; 
    public bool isRealmShifted = false;
    
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
        if (Input.GetKeyDown(KeyCode.R)|| Input.GetKeyDown(KeyCode.JoystickButton5)) 
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
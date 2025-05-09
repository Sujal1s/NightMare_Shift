using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialboxCanvas;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image portraitImage;

    [Header("Dialogue Data")]
    [SerializeField] private string[] speakers;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private Sprite[] portraits;

    [Header("Player")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private ParticleSystem playerParticle;

    [Header("Settings")]
    [Tooltip("Typing speed in seconds per character")]
    public float typingSpeed = 0.05f;
    [Tooltip("If false, dialogue only triggers once")]
    public bool canTriggerAgain = false;

    private PlayerController playerController;
    private Animator anim;
    private Collider2D triggerCollider;

    internal bool dialogueActive = false;
    private bool hasTriggered = false;
    private int step = 0;
    public int EnableAtStep = 6;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        triggerCollider = GetComponent<Collider2D>();

        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerController = playerObj.GetComponent<PlayerController>();
        else
            Debug.LogWarning("[DialogueSystem] No GameObject with tag 'Player' found.");
    }

    private void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            if (step >= dialogueLines.Length)
            {
                CloseDialogue();
                return;
            }

            ShowLine(step);
            step++;
        }

        // re‐enable player once we hit a certain step
        if (step >= EnableAtStep && playerController != null && !playerController.enabled)
        {
            playerController.enabled = true;
            playerAnimator?.SetBool("_ismoving", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // only the player can start it
        if (!col.CompareTag("Player")) 
            return;

        // if we already ran and shouldn't run again, bail
        if (hasTriggered && !canTriggerAgain) 
            return;

        hasTriggered = true;
        dialogueActive = true;
        step = 0;

        // play your reverse/open animation
        anim?.SetTrigger("PlayReverse");

        // disable player movement
        playerController.enabled = false;
        playerAnimator?.SetBool("_ismoving", false);
        playerParticle?.Stop();

        ShowLine(step);
        step++;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) 
            return;
        
        CloseDialogue();

        // if we don’t want to ever retrigger, disable the collider
        if (!canTriggerAgain)
            triggerCollider.enabled = false;
    }

    private void ShowLine(int index)
    {
        if (dialboxCanvas != null)
            dialboxCanvas.SetActive(true);

        // set speaker & portrait immediately
        speakerText.text  = index < speakers.Length  ? speakers[index]  : "";
        portraitImage.sprite = index < portraits.Length ? portraits[index] : null;

        // kick off the typing effect
        StopAllCoroutines();  // clear any running TypeText
        string line = index < dialogueLines.Length ? dialogueLines[index] : "";
        StartCoroutine(TypeText(dialogueText, line));
    }

    private void CloseDialogue()
    {
        dialogueActive = false;
        if (dialboxCanvas != null)
            dialboxCanvas.SetActive(false);

        anim?.SetTrigger("PlayDisappear");
    }

    private IEnumerator TypeText(TextMeshProUGUI textComponent, string textToType)
    {
        textComponent.text = "";
        
        for (int i = 0; i < textToType.Length; i++)
        {
            textComponent.text += textToType[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}

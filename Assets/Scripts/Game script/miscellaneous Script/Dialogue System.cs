using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private PlayerController playerController;
    private Animator anim;
    
    internal bool dialogueActive = false;
    private int step = 0;

    public int EnableAtStep = 6;

    private void Awake()
    {
        anim = GetComponent<Animator>();

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
            if (step >= speakers.Length || step >= dialogueLines.Length)
            {
                CloseDialogue();
                return;
            }

            ShowLine(step);
            step++;
        }

        if (step >= EnableAtStep && playerController != null && !playerController.enabled)
        {
            playerController.enabled = true;
            if (playerAnimator != null)
                playerAnimator.SetBool("_ismoving", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        dialogueActive = true;
        step = 0;
        anim?.SetTrigger("PlayReverse");

        if (playerController != null)
        {
            playerController.enabled = false;
            if (playerAnimator != null)
                playerAnimator.SetBool("_ismoving", false);
            playerParticle.Stop();
        }

        ShowLine(step);
        step++;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        CloseDialogue();
    }

    private void ShowLine(int index)
    {
        if (dialboxCanvas != null)
            dialboxCanvas.SetActive(true);

        speakerText.text  = (index < speakers.Length)      ? speakers[index]     : "";
        dialogueText.text = (index < dialogueLines.Length) ? dialogueLines[index] : "";
        portraitImage.sprite = (index < portraits.Length) ? portraits[index] : null;
    }

    private void CloseDialogue()
    {
        dialogueActive = false;
        if (dialboxCanvas != null)
            dialboxCanvas.SetActive(false);
        anim?.SetTrigger("PlayDisappear");
    }
}

using UnityEngine;
using TMPro;

public class PoneglyphReader : MonoBehaviour
{
    public GameObject promptUI;                // "Press Y" prompt
    public GameObject dialogueBoxUI;           // Dialogue box panel
    public TMP_Text dialogueTextUI;            // TextMeshPro text component
    public string[] dialogueLines;             // Array of dialogue lines

    private int currentIndex = 0;
    private bool isNear = false;
    private bool isReading = false;

    void Start()
    {
        promptUI.SetActive(false);
        dialogueBoxUI.SetActive(false);
        dialogueTextUI.text = "";
    }

    void Update()
    {
        // Only check input if player is near or reading
        if (isNear || isReading)
        {
            if (!isReading)
                promptUI.SetActive(true);

            if (Input.GetButtonDown("PoneglyphRead")) // <-- Controller Y or Keyboard Y
            {
                if (!isReading)
                    StartDialogue();
                else
                    NextLine();
            }
        }
        else
        {
            promptUI.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isNear = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            EndDialogueImmediate();
        }
    }

    void StartDialogue()
    {
        isReading = true;
        currentIndex = 0;
        promptUI.SetActive(false);
        dialogueBoxUI.SetActive(true);
        dialogueTextUI.text = dialogueLines[currentIndex];
    }

    void NextLine()
    {
        currentIndex++;
        if (currentIndex < dialogueLines.Length)
        {
            dialogueTextUI.text = dialogueLines[currentIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isReading = false;
        dialogueTextUI.text = "";
        dialogueBoxUI.SetActive(false);
        promptUI.SetActive(false);
    }

    void EndDialogueImmediate()
    {
        isReading = false;
        dialogueTextUI.text = "";
        dialogueBoxUI.SetActive(false);
        promptUI.SetActive(false);
    }
}

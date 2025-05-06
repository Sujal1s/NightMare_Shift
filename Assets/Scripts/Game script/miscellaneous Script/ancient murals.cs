using UnityEngine;
    using TMPro;
    using UnityEngine.InputSystem;
    using System.Collections;
    
    public class ancient_murals : MonoBehaviour
    {
        [SerializeField] GameObject promptUI;
        [SerializeField] GameObject dialogueBoxUI;
        [SerializeField] TMP_Text dialogueTextUI;
        [SerializeField] string[] dialogueLines;
        [SerializeField] float typingSpeed = 0.05f; // Typing speed in seconds
    
        private int currentIndex = 0;
        internal bool isNear = false;
        private bool isReading = false;
    
        private PlayerInputActions inputActions;
        private InputAction interactAction;
        private Coroutine typingCoroutine;
    
        void Awake()
        {
            inputActions = new PlayerInputActions();
            interactAction = inputActions.Player.Interact;
        }
    
        void OnEnable()
        {
            interactAction.Enable();
        }
    
        void OnDisable()
        {
            interactAction.Disable();
        }
    
        void Start()
        {
            promptUI.SetActive(false);
            dialogueBoxUI.SetActive(false);
            dialogueTextUI.text = "";
        }
    
        void Update()
        {
            if (isNear || isReading)
            {
                if (!isReading)
                    promptUI.SetActive(true);
    
                if (interactAction.WasPressedThisFrame())
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
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeText(dialogueTextUI, dialogueLines[currentIndex]));
        }
    
        void NextLine()
        {
            currentIndex++;
            if (currentIndex < dialogueLines.Length)
            {
                if (typingCoroutine != null) StopCoroutine(typingCoroutine);
                typingCoroutine = StartCoroutine(TypeText(dialogueTextUI, dialogueLines[currentIndex]));
            }
            else
            {
                EndDialogue();
            }
        }
    
        void EndDialogue()
        {
            isReading = false;
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            dialogueTextUI.text = "";
            dialogueBoxUI.SetActive(false);
            promptUI.SetActive(false);
        }
    
        void EndDialogueImmediate()
        {
            isReading = false;
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            dialogueTextUI.text = "";
            dialogueBoxUI.SetActive(false);
            promptUI.SetActive(false);
        }
    
        private IEnumerator TypeText(TMP_Text textComponent, string textToType)
        {
            textComponent.text = "";
            for (int i = 0; i < textToType.Length; i++)
            {
                textComponent.text += textToType[i];
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
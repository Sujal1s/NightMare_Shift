using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialboxcanvas;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI speakertext;
    [SerializeField] private TextMeshProUGUI dialougeText;
    [SerializeField] private Image potraiteImage;

    [SerializeField] private string[] speaker;
    [SerializeField] private string[] dialougeWord;
    [SerializeField] private Sprite[] potrait;
    private Animator animator;


    public int step;
    private bool dialougeactive;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton3)&& dialougeactive || Input.GetKeyDown(KeyCode.E) && dialougeactive)
        {
            if (step >= speaker.Length)
            {
                dialboxcanvas.SetActive(false);
                
                return;
            }
            else
            {
                dialboxcanvas.SetActive(true);
                speakertext.text = speaker[step];
                dialougeText.text = dialougeWord[step];
                potraiteImage.sprite = potrait[step];
                step += 1;
            }
        }

        if ( step > 6 )
        {
            PlayerController playerScript = player.GetComponent<PlayerController>();
            if (playerScript != null)
            {
                playerScript.enabled = !playerScript.enabled;
            }
            else
            {
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("PlayReverse");
            dialougeactive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        dialougeactive = false;
        dialboxcanvas.SetActive(false);
        animator.SetTrigger("PlayDisappear");
    }


}
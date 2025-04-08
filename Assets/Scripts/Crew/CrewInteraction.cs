using System.Collections;
using TMPro;
using UnityEngine;

public class CrewInteraction : MonoBehaviour, IInteractable
{   
    public NPCDialog dialogData;
    public GameObject dialogPanel;
    public TMP_Text dialogText, nameText;
    private int dialogIndex;
    private bool isTyping = false;
    private bool isDialogActive = false;

    public void Interact()
    {
        if (dialogData == null)
        {
            return;
        }

        if (isDialogActive) {
            // Next dialog line
            NextLine();
        } else {
            // Start dialog
            StartDialog();
        }
    }

    void StartDialog()
    {
        isDialogActive = true;
        dialogIndex = 0;
        //nameText.text = dialogData.npcName;
        dialogPanel.SetActive(true);
        
        StartCoroutine(TypeLine());
    }

    void NextLine() {
        if (isTyping) {
            // Skip typing animation and show full line immediately
            StopAllCoroutines();
            dialogText.text = dialogData.dialogueLines[dialogIndex];
            isTyping = false;
        } else if (++dialogIndex < dialogData.dialogueLines.Length) {
            StartCoroutine(TypeLine());
        } else {
            // End dialog
            EndDialog();
        }
    }
    IEnumerator TypeLine() {
        isTyping = true;
        dialogText.text = string.Empty;
        foreach (char letter in dialogData.dialogueLines[dialogIndex].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(dialogData.typingSpeed);
        }
        isTyping = false;

        // Check if the current line should auto-progress
        if (dialogData.autoProgressLines.Length > dialogIndex && dialogData.autoProgressLines[dialogIndex])
        {
            yield return new WaitForSeconds(dialogData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialog()
    {
        StopAllCoroutines();
        isDialogActive = false;
        dialogPanel.SetActive(false);
        dialogText.text = string.Empty;
        // nameText.text = string.Empty;
    }

    public bool IsInteractable()
    {
        // Add logic to determine if the NPC is interactable
        return true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize dialog panel and text components
        dialogPanel.SetActive(false);
        dialogText.text = string.Empty;
        dialogIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

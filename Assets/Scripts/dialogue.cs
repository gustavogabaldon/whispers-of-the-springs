using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Needed for Image

public class dialogue : MonoBehaviour
{
    [Header("Dialogue UI")]
    public TMP_Text dialogueText;   // Drag your TMP text here
    public Image dialogueBox;       // Drag your background panel/image here

    [Header("Dialogue Settings")]
    [TextArea] public string dialogueLine;     // Line to show when near
    public float typingSpeed = 0.05f;          // Delay between each character
    public float displayDuration = 3f;         // How long text stays after finishing

    private bool isPlayerNear = false;
    private Coroutine typingCoroutine;

    private void Start()
    {
        HideDialogue();  // Ensures dialogue box and text are hidden at the start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            ShowDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            HideDialogue();
        }
    }

    void ShowDialogue()
    {
        Debug.Log("IN show dialogue");

        if (dialogueBox != null) dialogueBox.enabled = true; // Show background

        // Stop any ongoing typing coroutine before starting a new one
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(dialogueLine));
    }

    IEnumerator TypeText(string line)
    {
        if (dialogueText == null) yield break;

        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Wait for the display duration, then clear
        yield return new WaitForSeconds(displayDuration);

        if (!isPlayerNear)
            HideDialogue();
    }

    void HideDialogue()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        if (dialogueText != null) dialogueText.text = "";
        if (dialogueBox != null) dialogueBox.enabled = false;
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private Coroutine typingCoroutine;
    public float typeSpeed = 0.02f;
    private bool isTyping = false;
    public Image portraitImage; // assign this in Inspector
    public CanvasGroup dialoguePanelGroup; // Assign in Inspector
    public GameObject dimBackground; // Assign in Inspector


    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerNameText;

    private Queue<DialogueLine> dialogueQueue = new Queue<DialogueLine>();
    private bool isDialogueActive = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        dialoguePanel.SetActive(false);

        if (dimBackground != null)
        {
            dimBackground.SetActive(false);
            dimBackground.GetComponent<CanvasGroup>().alpha = 0f;
        }
    }


    public void StartDialogue(List<DialogueLine> lines)
    {
        dimBackground.SetActive(true);

        if (lines == null || lines.Count == 0)
        {
            Debug.LogWarning("No dialogue lines to show.");
            return;
        }

        dialogueQueue.Clear();
        foreach (var line in lines)
        {
            dialogueQueue.Enqueue(line);
        }

        dialoguePanel.SetActive(true);
        dialoguePanelGroup.alpha = 0f;
        dialoguePanelGroup.transform.localScale = Vector3.zero;

        LeanTween.alphaCanvas(dialoguePanelGroup, 1f, 0.3f).setEaseOutCubic();
        LeanTween.scale(dialoguePanelGroup.gameObject, Vector3.one, 0.3f).setEaseOutBack();

        dimBackground.SetActive(true);
        dimBackground.GetComponent<CanvasGroup>().alpha = 0f;
        LeanTween.alphaCanvas(dimBackground.GetComponent<CanvasGroup>(), 1f, 0.3f);


        isDialogueActive = true;
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = dialogueQueue.Dequeue();
        speakerNameText.text = currentLine.speakerName;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        portraitImage.sprite = currentLine.portrait;


        typingCoroutine = StartCoroutine(TypeLine(currentLine.lineText));

        IEnumerator TypeLine(string line)
        {
            isTyping = true;
            dialogueText.text = "";

            foreach (char c in line.ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSecondsRealtime(typeSpeed);
            }

            isTyping = false;
        }

    }


    private void Update()
    {
        if (!isDialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // Skip typing effect and show full line immediately
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogueQueue.Peek().lineText; // preview next
                typingCoroutine = null;
                isTyping = false;
            }
            else
            {
                ShowNextLine();
            }
        }
    }


    private void EndDialogue()
    {
        isDialogueActive = false;
        LeanTween.alphaCanvas(dialoguePanelGroup, 0f, 0.25f).setEaseInCubic();
        LeanTween.scale(dialoguePanelGroup.gameObject, Vector3.zero, 0.25f).setEaseInBack()
            .setOnComplete(() => dialoguePanel.SetActive(false));

        LeanTween.alphaCanvas(dimBackground.GetComponent<CanvasGroup>(), 0f, 0.3f)
    .setOnComplete(() => dimBackground.SetActive(false));

    }

    public bool IsDialogueActive() => isDialogueActive;
    public static bool DialogueIsActive => instance != null && instance.isDialogueActive;

}

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

    [Header("Choices")]
    public GameObject choicePanel;
    public GameObject choiceButtonPrefab;
    private int selectedIndex;

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

        // Show choices if this line has them
        if (currentLine.choices != null && currentLine.choices.Length > 0)
        {
            StartCoroutine(ShowChoices(currentLine.choices));
            return;
        }

    }

    private IEnumerator ShowChoices(DialogueLine.Choice[] choices)
    {
        isDialogueActive = false; // pause dialogue flow
        choicePanel.SetActive(true);

        // Remove previous buttons
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Create buttons
        for (int i = 0; i < choices.Length; i++)
        {
            var choice = choices[i];
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choicePanel.transform);
            TextMeshProUGUI btnText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = $"{i + 1}. {choice.choiceText}";

            int index = i;
            buttonObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnChoiceSelected(choices[index]);
            });

        }

        yield return null;
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

        if (choicePanel.activeSelf)
        {
            for (int i = 0; i < 9; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    var buttons = choicePanel.GetComponentsInChildren<Button>();
                    if (i < buttons.Length)
                    {
                        buttons[i].onClick.Invoke();
                    }
                }
            }

            return;
        }

    }

    private void OnChoiceSelected(DialogueLine.Choice choice)
    {
        choicePanel.SetActive(false);
        dialogueQueue.Clear();

        // Trigger frame swap if defined
        if (choice.frameIndexToSet >= 0)
        {
            UIFrameManager.instance.ChangeFrame(choice.frameIndexToSet);
        }

        if (choice.nextLine != null)
        {
            dialogueQueue.Enqueue(choice.nextLine);
            isDialogueActive = true;
            ShowNextLine();
        }
        else
        {
            EndDialogue();
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

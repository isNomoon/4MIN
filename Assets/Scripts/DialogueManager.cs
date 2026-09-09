using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;

        [TextArea(2, 5)]
        public string dialogueText;
    }

    [Header("UI References")]
    public GameObject dialoguePanel;
    public TMP_Text speakerNameText;
    public TMP_Text dialogueText;
    public TMP_Text continueHint;

    [Header("Dialogue Settings")]
    public DialogueLine[] dialogueLines;

    [Range(0.01f, 0.2f)]
    public float typingSpeed = 0.04f;

    [Header("Input Settings")]
    public KeyCode advanceKey = KeyCode.Mouse0;

    private int currentLineIndex = 0;

    private bool isTyping = false;

    private bool dialogueFinished = false;

    private Coroutine typingCoroutine;

    private string currentFullText;

    private void Start()
    {
        StartDialogue();
    }

    private void Update()
    {
        if (dialogueFinished)
        {
            return;
        }

        if (Input.GetKeyDown(advanceKey))
        {
            HandleAdvanceInput();
        }
    }

    public void StartDialogue()
    {
        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogWarning("DialogueManager: 没有配置任何对话内容。");
            return;
        }

        currentLineIndex = 0;
        dialogueFinished = false;

        dialoguePanel.SetActive(true);

        ShowCurrentLine();
    }

    private void HandleAdvanceInput()
    {
        if (isTyping)
        {
            CompleteCurrentText();
        }
        else
        {
            ShowNextLine();
        }
    }

    private void ShowCurrentLine()
    {
        if (currentLineIndex >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueLines[currentLineIndex];

        speakerNameText.text = line.speakerName;

        currentFullText = line.dialogueText;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(currentFullText));
    }

    private IEnumerator TypeText(string textToType)
    {
        isTyping = true;

        dialogueText.text = "";

        continueHint.text = "点击显示完整文字 ▶";

        foreach (char letter in textToType)
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        continueHint.text = "点击继续 ▶";
    }

    private void CompleteCurrentText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueText.text = currentFullText;

        isTyping = false;

        continueHint.text = "点击继续 ▶";
    }

    private void ShowNextLine()
    {
        currentLineIndex++;

        ShowCurrentLine();
    }

    private void EndDialogue()
    {
        dialogueFinished = true;

        isTyping = false;

        dialogueText.text = "剧情结束";

        continueHint.text = "";

        Debug.Log("DialogueManager: 剧情结束。");
    }
}
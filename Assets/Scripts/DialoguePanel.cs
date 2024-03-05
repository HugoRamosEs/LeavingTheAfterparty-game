using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    public enum DialogueState
    {
        Writing,
        Finished
    }

    private Dialogue dialogue;
    private DialogueState dialogueState = DialogueState.Finished;

    public TextMeshProUGUI npcText;
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private Image npcImage;
    [SerializeField] private float timeChar = 0.05f;

    private int lIndex;
    private string[] dLines;

    void Update()
    {
        if (dialogue == null)
        {
            CheckForDialogue();
        }
    }

    public void UpdateValues(Dialogue dialogue, string[] dialogueLines, int lineIndex)
    {
        this.dialogue = dialogue;
        npcName.text = dialogue.npcName;
        npcImage.sprite = dialogue.npcImage;
        dLines = dialogueLines;
        lIndex = lineIndex;

        Time.timeScale = 0f;
        StartWritingLine();
    }

    public void NextDialogLine()
    {
        if (dialogueState == DialogueState.Writing)
        {
            StopAllCoroutines();
            npcText.text = dLines[lIndex];
            dialogueState = DialogueState.Finished;
        }
        else
        {
            lIndex++;
            if (lIndex < dLines.Length)
            {
                StartWritingLine();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F)) {
                    dialogue.EndDialogue();
                    Time.timeScale = 1f;
                }
            }
        }
    }

    IEnumerator ShowLine()
    {
        dialogueState = DialogueState.Writing;
        npcText.text = string.Empty;
        foreach (char ch in dLines[lIndex])
        {
            npcText.text += ch;
            yield return new WaitForSecondsRealtime(timeChar);
        }
        dialogueState = DialogueState.Finished;
    }

    private void StartWritingLine()
    {
        StopAllCoroutines();
        StartCoroutine(ShowLine());
    }

    void CheckForDialogue()
    {
        Dialogue foundDialogue = FindObjectOfType<Dialogue>();

        if (foundDialogue != null)
        {
            dialogue = foundDialogue;
        }
    }
}

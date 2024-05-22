using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This method is responsible for the dialogue panel, it will show the dialogue of the NPC that is talking to the player.
/// </summary>
public class DialoguePanel : MonoBehaviour
{
    
    /// <summary>
    /// This enum is responsible for the state of the dialogue, if it is writing or finished.
    /// </summary>
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

    /// <summary>
    /// This method is responsible for updating the dialogue panel, it will check for the dialogue
    /// </summary>
    void Update()
    {
        if (dialogue == null)
        {
            CheckForDialogue();
        }
    }

    /// <summary>
    /// This method is responsible for updating the values of the dialogue panel, it will update the name of the NPC, the image of the NPC, the dialogue lines and the line index.
    /// </summary>
    /// <param name="dialogue"> The dialogue item </param>
    /// <param name="dialogueLines"> The dialogue text</param>
    /// <param name="lineIndex"> The dialogue line</param>
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

    /// <summary>
    /// This method is responsible for the next dialogue line, it will check if the dialogue is writing or finished, if it is writing it will stop all coroutines and show the entire 
    /// line, if it is finished it will increment the line index and check if it is the last line, if it is the last line it will end the dialogue.
    /// </summary>
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
                if (Input.GetKeyDown(KeyCode.F))
                {
                    dialogue.EndDialogue();
                    Time.timeScale = 1f;
                }
            }
        }
    }

    /// <summary>
    /// This method is responsible for showing the entire line of the dialogue.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// This method is responsible for starting to write the line of the dialogue.
    /// </summary>
    private void StartWritingLine()
    {
        StopAllCoroutines();
        StartCoroutine(ShowLine());
    }

    /// <summary>
    /// This method is responsible for checking the reference of the dialogue.
    /// </summary>
    void CheckForDialogue()
    {
        Dialogue foundDialogue = FindObjectOfType<Dialogue>();

        if (foundDialogue != null)
        {
            dialogue = foundDialogue;
        }
    }
}

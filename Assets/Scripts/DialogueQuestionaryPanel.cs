using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueQuestionaryPanel : MonoBehaviour
{
    [SerializeField] private GameObject responsesPanel;
    [SerializeField] private Button[] responseButtons;
    private DialogueQuestionary dialogueQuestionary;

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

    void Start()
    {
        dialogueQuestionary = FindObjectOfType<DialogueQuestionary>();
    }

    void Update()
    {
        if (dialogue == null)
        {
            CheckForDialogue();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (dialogueQuestionary != null && !dialogueQuestionary.IsResponseDialogue && !dialogueQuestionary.isDialogueEnded && !dialogueQuestionary.isInitializing)
            {
                dialogueQuestionary.InitDialogue();
            }
            else if (dialogueQuestionary != null && dialogueQuestionary.isAnswerFeedback)
            {
                dialogueQuestionary.isAnswerFeedback = false;
                dialogue.EndDialogue();
                Time.timeScale = 1f;
            }
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

    public void ShowResponses(string[] responses)
    {
        for (int i = 0; i < responses.Length; i++)
        {
            int localI = i;
            responseButtons[i].gameObject.SetActive(true);
            responseButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = responses[i];
            responseButtons[i].onClick.AddListener(() => OnResponseButtonClicked(responses[localI]));
        }
        for (int i = responses.Length; i < responseButtons.Length; i++)
        {
            responseButtons[i].gameObject.SetActive(false);
        }
    }

    private void OnResponseButtonClicked(string response)
    {
        string nextDialogue = null;

        if (dialogueQuestionary != null && dialogueQuestionary.IsCorrectResponse(response))
        {
            nextDialogue = dialogueQuestionary.GetCorrectDialogue();
        }
        else
        {
            nextDialogue = dialogueQuestionary.GetIncorrectDialogue();
        }

        if (nextDialogue != null)
        {
            UpdateValues(dialogue, new string[] { nextDialogue }, 0);
            dialogueQuestionary.isAnswerFeedback = true;
        }

        if (dialogueQuestionary != null)
        {
            dialogueQuestionary.IsResponseDialogue = true;
        }

        foreach (Button button in responseButtons)
        {
            button.gameObject.SetActive(false);
        }
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

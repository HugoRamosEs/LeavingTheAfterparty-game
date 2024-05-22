using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for managing the dialogue questionary panel that appears when interacting with an NPC.
/// </summary>
public class DialogueQuestionaryPanel : MonoBehaviour
{
    [SerializeField] private GameObject responsesPanel;
    [SerializeField] private Button[] responseButtons;
    private DialogueQuestionary dialogueQuestionary;

    /// <summary>
    /// Represents the state of the dialogue.
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
    /// Find the DialogueQuestionary instance in the scene.
    /// </summary>
    void Start()
    {
        dialogueQuestionary = FindObjectOfType<DialogueQuestionary>();
    }

    /// <summary>
    /// This method is called when the object becomes enabled and active, and is used to initialize an instance of the dialogue questionary.
    /// </summary>
    void Awake()
    {
        
        dialogueQuestionary = DialogueQuestionary.Instance;
    }


    /// <summary>
    /// This method controle the interaction with the dialogue questionary panel on each frame.
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("[Update] Presionada tecla F. Estado isDialogueEnded: " + dialogueQuestionary.isDialogueEnded + ", isInitializing: " + dialogueQuestionary.isInitializing);

            if (dialogueQuestionary != null)
            {
                // Solo permitir la interacción si el diálogo no ha terminado o está inicializando.
                if (!dialogueQuestionary.isDialogueEnded && !dialogueQuestionary.isInitializing)
                {
                    Debug.Log("[Update] Continuando o iniciando diálogo porque no ha terminado ni está inicializando.");
                    dialogueQuestionary.InitDialogue();
                }
                else if (dialogueQuestionary.isDialogueEnded)
                {
                    Debug.Log("[Update] Intento de cerrar diálogo porque isDialogueEnded es true.");
                    EndDialogue();
                }
            }
        }
    }

    /// <summary>
    /// This method is responsible for updating the values of the dialogue questionary panel.
    /// </summary>
    /// <param name="dialogue"> The dialogue item</param>
    /// <param name="dialogueLines"> The dialogue text</param>
    /// <param name="lineIndex"> The line of the dialogue text</param>
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
    /// This method is responsible for displaying the next line of the dialogue.
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
        }
    }
    /// <summary>
    /// This method is responsible for displaying the dialogue line character by character.
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
    /// This method is responsible for starting to write the dialogue line.
    /// </summary>
    private void StartWritingLine()
    {
        StopAllCoroutines();
        StartCoroutine(ShowLine());
    }

    /// <summary>
    /// This method is responsible for displaying the responses to the dialogue question.
    /// </summary>
    /// <param name="responses">The responses available</param>
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

    /// <summary>
    /// This method is responsible for handling the response to the dialogue question.
    /// </summary>
    /// <param name="response"> The response of the NPC to the Player option selected</param>
    private void OnResponseButtonClicked(string response)
    {
        string nextDialogue = dialogueQuestionary.IsCorrectResponse(response) ?
                              dialogueQuestionary.GetCorrectDialogue() :
                              dialogueQuestionary.GetIncorrectDialogue();

        if (nextDialogue != null)
        {
            UpdateValues(dialogue, new string[] { nextDialogue }, 0);
            dialogueQuestionary.isAnswerFeedback = true;
            if (dialogueQuestionary.IsCorrectResponse(response) && dialogueQuestionary.relatedNPC != null)
            {
                dialogueQuestionary.relatedNPC.gameObject.SetActive(false);
            }
        }

        foreach (Button button in responseButtons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }

        dialogueQuestionary.isDialogueEnded = true;
        Debug.Log("==== EN DIALOGUEQUESTIONARYPANEL=======");
        Debug.Log("isdialogueended: " + dialogueQuestionary.isDialogueEnded);
        Debug.Log("isanswefeedback: " + dialogueQuestionary.isAnswerFeedback);
        Debug.Log("=======================================");
        dialogueQuestionary.forceState();
        responsesPanel.SetActive(false);
    }

    /// <summary>
    /// This method is responsible for ending the dialogue.
    /// </summary>
    public void EndDialogue()
    {
        if (dialogueQuestionary == null)
        {
            Debug.LogWarning("DialogueQuestionary reference lost, possibly due to scene unload.");
            return; 
        }

        if (!dialogueQuestionary.isDialogueEnded)
        {
            dialogueQuestionary.isDialogueEnded = true;
            Debug.Log("[EndDialogue] Marcando el diálogo como terminado.");
        }

        Debug.Log("[EndDialogue] Finalizando diálogo. Estado antes de resetear: isDialogueEnded: " + dialogueQuestionary.isDialogueEnded);
        responsesPanel.SetActive(false);
        Time.timeScale = 1f;
        gameObject.SetActive(false);

        if (dialogue != null)
        {
            dialogue.EndDialogue();
        }
        else
        {
            Debug.LogWarning("Missing dialogue reference when trying to end dialogue.");
        }

        dialogueQuestionary.ResetDialogueState();
        Debug.Log("[EndDialogue] Diálogo finalizado y estado reseteado.");
    }

    /// <summary>
    /// This method is responsible for activating the responses panel.
    /// </summary>
    public void ActivateResponsesPanel()
    {
        if (responsesPanel != null && !responsesPanel.activeSelf)
        {
            responsesPanel.SetActive(true);
        }
    }

    /// <summary>
    /// This method checks for the dialogue reference in the scene.
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

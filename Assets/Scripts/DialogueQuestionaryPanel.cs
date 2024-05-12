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

    void Awake()
    {
        // Siempre trabaja con la instancia única de DialogueQuestionary
        dialogueQuestionary = DialogueQuestionary.Instance;
    }


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
        string nextDialogue = dialogueQuestionary.IsCorrectResponse(response) ?
                              dialogueQuestionary.GetCorrectDialogue() :
                              dialogueQuestionary.GetIncorrectDialogue();

        if (nextDialogue != null)
        {
            UpdateValues(dialogue, new string[] { nextDialogue }, 0);
            dialogueQuestionary.isAnswerFeedback = true;
            if (dialogueQuestionary.IsCorrectResponse(response) && dialogueQuestionary.relatedNPC != null)
            {
                dialogueQuestionary.relatedNPC.SetActive(false);
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


    public void EndDialogue()
    {
        if (dialogueQuestionary == null)
        {
            Debug.LogWarning("DialogueQuestionary reference lost, possibly due to scene unload.");
            return; // Salir si el controlador de diálogo no está disponible.
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




    public void ActivateResponsesPanel()
    {
        if (responsesPanel != null && !responsesPanel.activeSelf)
        {
            responsesPanel.SetActive(true);
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
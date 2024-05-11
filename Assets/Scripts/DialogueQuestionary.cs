using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueQuestionary : Dialogue
{
    [SerializeField] private DialogueResponse[] dialogueResponses;
    public bool IsResponseDialogue { get; set; }
    public bool isDialogueEnded = false;
    public bool isInitializing = false;
    public bool isFKeyEnabled = true;
    public bool isAnswerFeedback = false;
    [SerializeField] private DialogueQuestionaryPanel dialogueQuestionaryPanel;
    public static DialogueQuestionary Instance;
    public GameObject relatedNPC;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Asegura que este objeto no se destruya al cargar nuevas escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (dialogueQuestionaryPanel == null)
        {
            CheckForDialogueQuestionaryPanel();
        }
        base.Update();
    }

    public override void InitDialogue()
    {
        if (isDialogueEnded)
        {
            Debug.Log("[InitDialogue] Diálogo intentó iniciar pero ya ha terminado.");
            return;  // Si el diálogo ha terminado, no hacer nada más.
        }

        CheckAndResetState();

        if (!didDialogueStart && !isInitializing)
        {
            ResetDialogueState();

            DialoguePanel regularDialoguePanel = FindObjectOfType<DialoguePanel>();
            if (regularDialoguePanel != null)
            {
                regularDialoguePanel.gameObject.SetActive(false);
            }

            if (dialogueQuestionaryPanel != null)
            {
                dialogueQuestionaryPanel.gameObject.SetActive(true);
                dialogueQuestionaryPanel.UpdateValues(this, dialogueLines, lineIndex);
                dialogueQuestionaryPanel.ActivateResponsesPanel();
            }
            else
            {
                Debug.LogError("DialogueQuestionaryPanel no está asignado en el inspector.");
            }

            isInitializing = true;
            ShowResponses();
            isFKeyEnabled = false;
            StartDialogue();
            isInitializing = false;
        }
        else if (!isInitializing && isFKeyEnabled)
        {
            dialogueQuestionaryPanel.NextDialogLine();
        }
    }




    public void ResetDialogueState()
    {
        Debug.Log("[ResetDialogueState] Reseteando estado del diálogo.");
        isDialogueEnded = false;
        isAnswerFeedback = false;
        IsResponseDialogue = false;
        didDialogueStart = false;
        isFKeyEnabled = true;
        lineIndex = 0;
    }



    public void forceState()
    {

        Debug.Log("==== EN DIALOGUEQUESTIONARY=======");
        Debug.Log("isdialogueended: " + isDialogueEnded);
        Debug.Log("isanswefeedback: " + isAnswerFeedback);
        Debug.Log("=======================================");
    }


    public void OnPlayerReturnedToNPC()
    {
        // Solo reiniciar el estado si el diálogo ha terminado, para permitir reiniciar conversaciones.
        if (isDialogueEnded)
        {
            ResetDialogueState();  // Reinicia el estado para permitir nueva interacción.

            if (dialogueQuestionaryPanel != null)
            {
                dialogueQuestionaryPanel.gameObject.SetActive(true);
                dialogueQuestionaryPanel.UpdateValues(this, dialogueLines, 0);
                dialogueQuestionaryPanel.ActivateResponsesPanel();
            }
            else
            {
                Debug.LogError("DialogueQuestionaryPanel no está asignado o no se encontró");
            }
        }
    }





    public void CheckAndResetState()
    {
        if (isDialogueEnded)
        {
            Debug.Log("[CheckAndResetState] Intento de reseteo bloqueado porque el diálogo ya ha terminado.");
            return;  // Previene el reseteo si el diálogo ha terminado
        }

        if (!isDialogueEnded && !isAnswerFeedback)
        {
            Debug.Log("Resetting state due to inconsistency");
            ResetDialogueState();
        }
    }




    private void ShowResponses()
    {
        if (dialogueQuestionaryPanel == null)
        {
            foreach (var scene in SceneManager.GetAllScenes())
            {
                if (scene.isLoaded)
                {
                    var rootObjects = scene.GetRootGameObjects();
                    foreach (var obj in rootObjects)
                    {
                        dialogueQuestionaryPanel = obj.GetComponentInChildren<DialogueQuestionaryPanel>(true);
                        if (dialogueQuestionaryPanel != null)
                            break;
                    }
                    if (dialogueQuestionaryPanel != null)
                        break;
                }
            }

            if (dialogueQuestionaryPanel == null)
            {
                Debug.LogError("DialogueQuestionaryPanel is not found in any loaded scenes");
                return;
            }
        }

        if (dialogueResponses != null && dialogueResponses.Length > lineIndex && dialogueResponses[lineIndex] != null && dialogueResponses[lineIndex].responses != null)
        {
            dialogueQuestionaryPanel.ShowResponses(dialogueResponses[lineIndex].responses);
            isFKeyEnabled = true;
        }
    }

    public void OnPlayerApproaches()
    {

        ResetDialogueState();
        if (dialogueQuestionaryPanel != null)
        {
            dialogueQuestionaryPanel.UpdateValues(this, dialogueLines, 0);
        }
        else
        {

            CheckForDialogueQuestionaryPanel();
            if (dialogueQuestionaryPanel != null)
                dialogueQuestionaryPanel.UpdateValues(this, dialogueLines, 0);
        }
    }


    public bool IsCorrectResponse(string response)
    {
        return dialogueResponses != null && dialogueResponses.Length > lineIndex && dialogueResponses[lineIndex].correctResponse == response;

    }

    public string GetCorrectDialogue()
    {
        return dialogueResponses != null && dialogueResponses.Length > lineIndex ? dialogueResponses[lineIndex].correctDialogue : null;

    }

    public string GetIncorrectDialogue()
    {
        return dialogueResponses != null && dialogueResponses.Length > lineIndex ? dialogueResponses[lineIndex].incorrectDialogue : null;
    }
    private void CheckForDialogueQuestionaryPanel()
    {
        Scene esencialScene = SceneManager.GetSceneByName("EsencialScene");

        if (esencialScene.IsValid())
        {
            GameObject[] objectsInScene = esencialScene.GetRootGameObjects();
            foreach (GameObject obj in objectsInScene)
            {
                DialogueQuestionaryPanel foundPanel = obj.GetComponentInChildren<DialogueQuestionaryPanel>(true);
                if (foundPanel != null)
                {
                    dialogueQuestionaryPanel = foundPanel;
                    break;
                }
            }
        }
    }
}

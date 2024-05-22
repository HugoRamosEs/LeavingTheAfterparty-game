using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for the dialogue questionary, it will show the dialogue of the NPC that is talking and the possible responses.
/// </summary>
public class DialogueQuestionary : Dialogue
{
    [SerializeField] private DialogueResponse[] dialogueResponses;
    [SerializeField] private DialogueQuestionaryPanel dialogueQuestionaryPanel;

    public bool IsResponseDialogue { get; set; }
    public bool isDialogueEnded = false;
    public bool isInitializing = false;
    public bool isFKeyEnabled = true;
    public bool isAnswerFeedback = false;
    public Transform relatedNPC;
    public static DialogueQuestionary Instance;

    /// <summary>
    /// This method is responsible for the initialization of the dialogue, it will check if the dialogue has ended or not.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        relatedNPC = null;
        CheckForPlayerWithTag();
    }

    /// <summary>
    /// This method is responsible for updating the dialogue questionary, it will check for the dialogue questionary panel.
    /// </summary>
    protected override void Update()
    {
        if (dialogueQuestionaryPanel == null)
        {
            CheckForDialogueQuestionaryPanel();
        }

        base.Update();
    }

    /// <summary>
    /// This method is responsible for the initialization of the dialogue.
    /// </summary>
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
    /// <summary>
    /// This method is responsible of ensuring that the dialogue state is reset.
    /// </summary>
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

    /// <summary>
    /// This method checks the state of isDialogueEnded and isAnswerFeedback.
    /// </summary>
    public void forceState()
    {
        Debug.Log("==== EN DIALOGUEQUESTIONARY=======");
        Debug.Log("isdialogueended: " + isDialogueEnded);
        Debug.Log("isanswefeedback: " + isAnswerFeedback);
        Debug.Log("=======================================");
    }

    /// <summary>
    /// This method ensures the retro-usability of the Questionary NPC when the player returns to this map after leaving it.
    /// </summary>
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

    /// <summary>
    /// This method is responsible for checking and resetting the state of the dialogue.
    /// </summary>
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

    /// <summary>
    /// This method is responsible for showing the responses of the dialogue.
    /// </summary>
    private void ShowResponses()
    {
        if (dialogueQuestionaryPanel == null)
        {

            // Old code (deprecated)
            ////foreach (var scene in SceneManager.GetAllScenes())
            ////{
            ////    if (scene.isLoaded)
            ////    {
            ////        var rootObjects = scene.GetRootGameObjects();
            ////        foreach (var obj in rootObjects)
            ////        {
            ////            dialogueQuestionaryPanel = obj.GetComponentInChildren<DialogueQuestionaryPanel>(true);
            ////            if (dialogueQuestionaryPanel != null)
            ////                break;
            ////        }
            ////        if (dialogueQuestionaryPanel != null)
            ////            break;
            ////    }
            ////}

            int countLoaded = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];
            for (int i = 0; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
            }

            foreach (var scene in loadedScenes)
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

    /// <summary>
    /// This method controle the dialogue when the player approaches the NPC.
    /// </summary>
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

    /// <summary>
    /// This method is responsible for checking if the response is correct.
    /// </summary>
    /// <param name="response"> the response selected </param>
    /// <returns> True or false depending on the response selected </returns>
    public bool IsCorrectResponse(string response)
    {
        return dialogueResponses != null && dialogueResponses.Length > lineIndex && dialogueResponses[lineIndex].correctResponse == response;

    }

    /// <summary>
    /// This method is responsible for getting the correct dialogue.
    /// </summary>
    /// <returns> the dialogue for the correct response selected</returns>
    public string GetCorrectDialogue()
    {
        return dialogueResponses != null && dialogueResponses.Length > lineIndex ? dialogueResponses[lineIndex].correctDialogue : null;

    }

    /// <summary>
    /// This method is responsible for getting the incorrect dialogue.
    /// </summary>
    /// <returns> the dialogue for the incorrect response selected</returns>
    public string GetIncorrectDialogue()
    {
        return dialogueResponses != null && dialogueResponses.Length > lineIndex ? dialogueResponses[lineIndex].incorrectDialogue : null;
    }

    /// <summary>
    /// This method is responsible for check the dialogue questionary panel reference.
    /// </summary>
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

    /// <summary>
    /// Check for the npc if its not found.
    /// </summary>
    void CheckForPlayerWithTag()
    {
        GameObject relatedNPC = GameObject.FindWithTag("Motorista-Hitbox");

        if (relatedNPC != null)
        {
            this.relatedNPC = relatedNPC.transform;
        } else
        {
            Debug.Log("Npc not found");
        }
    }
}

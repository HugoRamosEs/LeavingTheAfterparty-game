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
        if (!didDialogueStart && !isDialogueEnded)
        {
            // Desactiva el DialoguePanel regular
            DialoguePanel regularDialoguePanel = FindObjectOfType<DialoguePanel>();
            if (regularDialoguePanel != null)
                regularDialoguePanel.gameObject.SetActive(false);

            // Activa el DialogueQuestionaryPanel
            if (dialogueQuestionaryPanel != null)
            {
                dialogueQuestionaryPanel.gameObject.SetActive(true);
                dialogueQuestionaryPanel.UpdateValues(this, dialogueLines, lineIndex);  // Asegúrate de que se pasan los valores correctos aquí
            }
            else
            {
                Debug.LogError("DialogueQuestionaryPanel no está asignado en el inspector");
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




    private void ShowResponses()
    {
        if (dialogueQuestionaryPanel == null)
        {
            // Intenta encontrar el panel en todas las escenas cargadas
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

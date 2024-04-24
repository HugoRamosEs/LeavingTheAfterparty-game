using UnityEngine;

public class DialogueQuestionary : Dialogue
{
    [SerializeField] private DialogueResponse[] dialogueResponses;


    public override void InitDialogue()
    {
        if (!didDialogueStart)
        {
            StartDialogue();
        }
        else
        {
            dialoguePanel.NextDialogLine();
            ShowResponses();
        }
    }

    private void ShowResponses()
    {
        if (dialogueResponses != null && dialogueResponses.Length > lineIndex)
        {
            // Aquí puedes mostrar las respuestas en tu panel de diálogo
            // Por ejemplo:
            dialoguePanel.ShowResponses(dialogueResponses[lineIndex].responses);

        }
    }
}
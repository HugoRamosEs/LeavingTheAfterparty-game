using UnityEngine;
using TMPro;

public class DialoguePanelConfig : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;

 
    public void UpdateDialogueText(string text)
    {
        if (dialogueText != null)
        {
            dialogueText.text = text;
        }
    }
}

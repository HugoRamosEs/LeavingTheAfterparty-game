using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used to handle the dialogue of the NPC's in the game.
/// </summary>
public class Dialogue : MonoBehaviour
{
    public int lineIndex;

    public bool isPlayerInRange;
    public bool didDialogueStart;
    public bool requiresKeyPress = true;
    public DialoguePanel dialoguePanel;

    public GameObject dialogueMark;
    public string npcName;
    public Sprite npcImage;
    [SerializeField, TextArea(4, 6)] protected string[] dialogueLines;

    /// <summary>
    /// This method calls some methods for different purposes.
    /// </summary>
    protected virtual void Update()
    {
        if (dialoguePanel == null)
        {
            CheckForDialoguePanel();
        }

        if (isPlayerInRange)
        {
            if (requiresKeyPress && Input.GetKeyDown(KeyCode.F))
            {
                InitDialogue();
            }
            else if (!requiresKeyPress)
            {
                InitDialogue();
            }
        }
    }

    /// <summary>
    /// This method is used to initialize the dialogue.
    /// </summary>
    public virtual void InitDialogue()
    {
        if (!didDialogueStart)
        {
            StartDialogue();
        }
        else
        {
            dialoguePanel.NextDialogLine();
        }
    }

    /// <summary>
    /// This method is used to start the dialogue.
    /// </summary>
    public void StartDialogue()
    {
        lineIndex = 0;
        didDialogueStart = true;
        CheckForDialoguePanel();

        if (dialoguePanel != null)
        {
            dialoguePanel.gameObject.SetActive(true);
            if (dialogueMark != null)
            {
                dialogueMark.SetActive(false);
            }
            dialoguePanel.UpdateValues(this, dialogueLines, lineIndex);
        }
    }

    /// <summary>
    /// This method is used to end the dialogue.
    /// </summary>
    public void EndDialogue()
    {
        didDialogueStart = false;
        dialoguePanel.gameObject.SetActive(false);
        if (dialogueMark != null) {
            dialogueMark.SetActive(true);
        }
      
    }

    /// <summary>
    /// This method is used to check if the player is in range of the NPC.
    /// </summary>
    /// <param name="collision"> the Player's collision</param>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if(dialogueMark != null) {
                dialogueMark.SetActive(true);
            }
            
        }
    }

    /// <summary>
    /// This method is used to check if the player is out of range of the NPC.
    /// </summary>
    /// <param name="collision"> the player's collision </param>
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (dialogueMark != null)
            {
                dialogueMark.SetActive(false);
            }
        }
    }

    /// <summary>
    /// This method is used to check the reference of the DialoguePanel.
    /// </summary>
    private void CheckForDialoguePanel()
    {
        Scene esencialScene = SceneManager.GetSceneByName("EsencialScene");

        if (esencialScene.IsValid())
        {
            GameObject[] objectsInScene = esencialScene.GetRootGameObjects();

            foreach (GameObject obj in objectsInScene)
            {
                DialoguePanel foundDialoguePanel = obj.GetComponentInChildren<DialoguePanel>(true);

                if (foundDialoguePanel != null)
                {
                    dialoguePanel = foundDialoguePanel;
                    break;
                }
            }
        }
    }
}

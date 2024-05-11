using UnityEngine.SceneManagement;
using UnityEngine;

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

    public void EndDialogue()
    {
        didDialogueStart = false;
        dialoguePanel.gameObject.SetActive(false);
        if (dialogueMark != null) {
            dialogueMark.SetActive(true);
        }
      
    }

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
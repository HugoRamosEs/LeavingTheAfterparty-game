using UnityEngine.SceneManagement;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private int lineIndex;

    public bool isPlayerInRange;
    public bool didDialogueStart;
    public bool requiresKeyPress = true;
    private DialoguePanel dialoguePanel;

    public GameObject dialogueMark;
    public string npcName;
    public Sprite npcImage;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    void Update()
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

    public void InitDialogue()
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
            dialogueMark.SetActive(false);
            dialoguePanel.UpdateValues(this, dialogueLines, lineIndex);
        }
    }

    public void EndDialogue()
    {
        didDialogueStart = false;
        dialoguePanel.gameObject.SetActive(false);
        dialogueMark.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
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
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class doorBarController : MonoBehaviour
{
    private ItemPanel itemPanel;
    private bool hasKey = false;

    [SerializeField] LightController lightController;
    [SerializeField] string message;
    [SerializeField] GameObject dialogueGame;

    private DialogueGame dialogueScript;

    private void Start()
    {
        dialogueScript = dialogueGame.GetComponent<DialogueGame>();
    }

    private void Update()
    {
        if (itemPanel == null)
        {
            CheckForDialoguePanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if (lightController.isDark)
            //{
            //    dialogueGame.gameObject.SetActive(true);
            //    dialogueScript.UpdateText(message);
            //}

            foreach (ItemSlot slot in itemPanel.inventory.slots)
            {
                if (slot.item != null && slot.item.Name == "Key")
                {
                    hasKey = true;
                    break;
                }
            }

            if (!hasKey)
            {
                dialogueGame.gameObject.SetActive(true);
                dialogueScript.UpdateText("Press F to open the door");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueScript.StopText();
            dialogueGame.gameObject.SetActive(false);
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
                ItemPanel foundItemPanel = obj.GetComponentInChildren<ItemPanel>(true);

                if (foundItemPanel != null)
                {
                    itemPanel = foundItemPanel;
                    break;
                }
            }
        }
    }
}

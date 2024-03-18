using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToBarController : MonoBehaviour
{
    private ItemPanel itemPanel;
    private bool hasKey = false;

    [SerializeField] LightController lightController;
    [SerializeField] GameObject dialogueGame;
    [SerializeField] Collider2D doorCollider;

    private DialogueGame dialogueScript;

    private void Start()
    {
        dialogueScript = dialogueGame.GetComponent<DialogueGame>();
        itemPanel = null;
        CheckForDialoguePanel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ItemSlot slot in itemPanel.inventory.slots)
            {
                if (slot.item != null && slot.item.Name == "Key")
                {
                    hasKey = true;
                    break;
                }
            }

            if (lightController.isDark && hasKey)
            {
                dialogueGame.gameObject.SetActive(true);
                dialogueScript.UpdateText("Estaría bien que encendieras las luz antes de seguir...");
            }
            if (!hasKey && !lightController.isDark)
            {
                dialogueGame.gameObject.SetActive(true);
                dialogueScript.UpdateText("La puerta esta bloqueada, necesitas la llave para continuar...");
            }
            if (!hasKey && lightController.isDark)
            {
                dialogueGame.gameObject.SetActive(true);
                dialogueScript.UpdateText("Necesitas encender la luz y la llave que abre la puerta para poder continuar...");
            }
            if (hasKey && !lightController.isDark)
            {
                doorCollider.gameObject.SetActive(false);
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

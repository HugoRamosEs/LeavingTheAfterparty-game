using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToBarController : MonoBehaviour
{
    private ItemPanel itemPanel;
    private bool hasKey = false;
    private PlayerSceneController player;

    [SerializeField] LightController lightController;
    [SerializeField] GameObject dialogueGame;
    [SerializeField] Collider2D doorCollider;

    private DialogueGame dialogueScript;

    private void Start()
    {
        dialogueScript = dialogueGame.GetComponent<DialogueGame>();
        itemPanel = null;
        player = null;
        CheckForPlayerSceneController();
        CheckForDialoguePanel();

        if (player.sotanoPasado)
        {
            hasKey = true;
            lightController.isDark = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ItemSlot slot in itemPanel.inventory.slots)
            {
                if (slot.item != null && slot.item.Name == "LlaveSotano")
                {
                    hasKey = true;
                    break;
                }
            }

            if (lightController.isDark && hasKey)
            {
                dialogueGame.gameObject.SetActive(true);
                dialogueScript.UpdateText("Estar�a bien que encendieras la luz antes de seguir...");
            }
            if (!hasKey && !lightController.isDark)
            {
                dialogueGame.gameObject.SetActive(true);
                dialogueScript.UpdateText("La puerta est� bloqueada, necesitas la llave para continuar...");
            }
            if (!hasKey && lightController.isDark)
            {
                dialogueGame.gameObject.SetActive(true);
                dialogueScript.UpdateText("Debes encender la luz y conseguir la llave que abre la puerta para poder continuar.");
            }
            if (hasKey && !lightController.isDark)
            {
                doorCollider.gameObject.SetActive(false);
                player.sotanoPasado = true;
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

    private void CheckForPlayerSceneController()
    {
        Scene esencialScene = SceneManager.GetSceneByName("EsencialScene");

        if (esencialScene.IsValid())
        {
            GameObject[] objectsInScene = esencialScene.GetRootGameObjects();

            foreach (GameObject obj in objectsInScene)
            {
                PlayerSceneController foundPlayerSceneController = obj.GetComponentInChildren<PlayerSceneController>(true);

                if (foundPlayerSceneController != null)
                {
                    player = foundPlayerSceneController;
                    break;
                }
            }
        }
    }
}

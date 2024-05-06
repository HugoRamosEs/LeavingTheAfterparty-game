using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToBarController : MonoBehaviour
{
    private bool hasKey = false;
    private ItemPanel itemPanel;
    private DialogueGame dialogueScript;

    [SerializeField] LightController lightController;
    [SerializeField] GameObject dialogueGame;
    [SerializeField] Collider2D doorCollider;

    private void Start()
    {
        dialogueScript = dialogueGame.GetComponent<DialogueGame>();
        itemPanel = null;
        CheckForItemPanel();

        if (PlayerSceneController.sotanoPasado)
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
                dialogueScript.UpdateText("Estaría bien que encendieras la luz antes de seguir...");
            }
            if (!hasKey && !lightController.isDark)
            {
                dialogueGame.gameObject.SetActive(true);
                dialogueScript.UpdateText("La puerta está bloqueada, necesitas la llave para continuar...");
            }
            if (!hasKey && lightController.isDark)
            {
                dialogueGame.gameObject.SetActive(true);
                dialogueScript.UpdateText("Debes encender la luz y conseguir la llave que abre la puerta para poder continuar.");
            }
            if (hasKey && !lightController.isDark)
            {
                doorCollider.gameObject.SetActive(false);
                PlayerSceneController.sotanoPasado = true;
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

    private void CheckForItemPanel()
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

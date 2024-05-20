using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for managing the door to the bar interaction.
/// </summary>
public class DoorToBarController : MonoBehaviour
{
    private bool hasKey = false;
    private ItemPanel itemPanel;
    private DialogueGame dialogueScript;

    [SerializeField] LightController lightController;
    [SerializeField] GameObject dialogueGame;
    [SerializeField] Collider2D doorCollider;

    /// <summary>
    /// This method is used to initialize the dialogue game and check if the player has the key to open the door.
    /// </summary>
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

    /// <summary>
    /// This method is called when the player enters the collider of the door to the bar.
    /// </summary>
    /// <param name="collision"></param>
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

    /// <summary>
    /// This method is called when the player exits the collider of the door to the bar.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueScript.StopText();
            dialogueGame.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Check for the reference of the item panel in the EsencialScene.
    /// </summary>
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

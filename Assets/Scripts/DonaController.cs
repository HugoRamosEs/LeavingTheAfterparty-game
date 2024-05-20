using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This method is responsible for managing the dona map interaction.
/// </summary>
public class DonaController : MonoBehaviour
{
    private bool hasSugarSack = false;
    private ItemPanel itemPanel;
    public GameObject tpToCiudad;
    public GameObject donaPanel;
    public DialogueGame dialogueGame;

    /// <summary>
    /// This method is called when the object becomes enabled and active, and is used to initialize the item panel.
    /// </summary>
    private void Start()
    {
        itemPanel = null;
        CheckForItemPanel();
    }

    /// <summary>
    /// This method is called to check if the player has the recipe in the inventory.
    /// </summary>
    /// <param name="collision"> The collision of the Player </param>
    /// <returns></returns>
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ItemSlot slot in itemPanel.inventory.slots)
            {
                if (slot.item != null && slot.item.Name == "receta")
                {
                    hasSugarSack = true;
                    break;
                }
            }

            if (!hasSugarSack)
            {
                yield return new WaitForSecondsRealtime(0.80f);
                Time.timeScale = 0f;
                donaPanel.SetActive(true);
                dialogueGame.UpdateText("¡¡¡ESTÁ CERRADO!!! Necesito un esa receta antes de abrir...");
                yield return new WaitForSecondsRealtime(4.75f);
                Time.timeScale = 1f;
                tpToCiudad.SetActive(true);
            }
            if (hasSugarSack && PlayerSceneController.donutDesbloqueado == false) 
            {
                yield return new WaitForSecondsRealtime(0.80f);
                Time.timeScale = 0f;
                donaPanel.SetActive(true);
                dialogueGame.UpdateText("¡¡¡ESTÁ CERRADO! Un momento... ¿Esa es mi receta verdad? ¡Por fin!");
                PlayerSceneController.donutDesbloqueado = true;
                yield return new WaitForSecondsRealtime(4.75f);
                donaPanel.SetActive(false);
                Time.timeScale = 1f;

            }
        }
    }
    /// <summary>
    /// Check for the item panel in the Esencialscene.
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

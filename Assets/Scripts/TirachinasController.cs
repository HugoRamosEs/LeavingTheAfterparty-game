using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is used to check if the player has the slingshot in their inventory.
/// </summary>
public class TirachinasController : MonoBehaviour
{
    private bool hasTirachinas;
    private ItemPanel itemPanel;

    public GameObject tirachinas;

    /// <summary>
    /// This method is used to call the CheckForItemPanel and CheckInventoryItems methods at the start of the script execution.
    /// </summary>
    void Start()
    {
        itemPanel = null;
        CheckForItemPanel();
        CheckInventoryItems();
    }

    /// <summary>
    /// This method is used to check for the inventory item in the player's inventory.
    /// </summary>
    void CheckInventoryItems()
    {
        foreach (ItemSlot slot in itemPanel.inventory.slots)
        {
            if (slot.item != null && slot.item.Name == "tirachinas")
            {
                hasTirachinas = true;
            }
        }

        if (hasTirachinas)
        {
            tirachinas.SetActive(false);
        }
        else
        {
            tirachinas.SetActive(true);
        }
    }

    /// <summary>
    /// This method is used to check if the player has the item panel.
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

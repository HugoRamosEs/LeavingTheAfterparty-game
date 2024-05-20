using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script that unlocks a certain area when the player has a certain item in their inventory.
/// </summary>
public class DesbloqueoScript : MonoBehaviour
{
    private ItemPanel itemPanel;
    public GameObject hitbox;
    public GameObject muros;
    /// <summary>
    /// Call the CheckForItemPanel method.
    /// </summary>
    private void Start()
    {
        itemPanel = null;
        CheckForItemPanel();
    }

    /// <summary>
    /// If the player has the item "Donuts" in the inventory, the hitbox and walls are disabled.
    /// </summary>
    /// <param name="collision"> the Player collision </param>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ItemSlot slot in itemPanel.inventory.slots)
            {
                if (slot.item != null && slot.item.Name == "Donuts")
                {
                   hitbox.SetActive(false);
                    muros.SetActive(false);
                    break;
                }
            }   

        }
    }
    /// <summary>
    /// Check for the reference of the ItemPanel in the EsencialScene.
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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventoryController : MonoBehaviour
{
    private ItemPanel itemPanel;

    public void Start()
    {
        CheckForItemPanel();
    }

    public List<(int, Item)> GetInventoryItems()
    {
        List<(int, Item)> inventoryItems = new List<(int, Item)>();

        for (int i = 0; i < itemPanel.inventory.slots.Count; i++)
        {
            ItemSlot slot = itemPanel.inventory.slots[i];
            if (slot.item != null)
            {
                inventoryItems.Add((i, slot.item));
            }
        }

        return inventoryItems;
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
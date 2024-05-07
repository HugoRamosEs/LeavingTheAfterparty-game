using UnityEngine;
using UnityEngine.SceneManagement;

public class TirachinasController : MonoBehaviour
{
    private bool hasTirachinas;
    private ItemPanel itemPanel;

    public GameObject tirachinas;

    void Start()
    {
        itemPanel = null;
        CheckForItemPanel();
        CheckInventoryItems();
    }

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

using UnityEngine;
using UnityEngine.SceneManagement;

public class CongeladorController : MonoBehaviour
{
    private bool hasKey = false;
    private ItemPanel itemPanel;

    public GameObject key;

    void Start()
    {
        itemPanel = null;
        CheckForItemPanel();

        if (PlayerSceneController.congeladorPasado)
        {
            key.SetActive(false);
        }
    }

    void Update()
    {
        if (!hasKey)
        {
            foreach (ItemSlot slot in itemPanel.inventory.slots)
            {
                if (slot.item != null && slot.item.Name == "LlaveSotano")
                {
                    hasKey = true;
                    break;
                }
            }
        }
        else
        {
            PlayerSceneController.congeladorPasado = true;
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

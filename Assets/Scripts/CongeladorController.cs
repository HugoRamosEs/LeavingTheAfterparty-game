using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller for the Freezer room. It checks if the player has the key to the basement and if so, it sets the key as inactive.
/// </summary>
public class CongeladorController : MonoBehaviour
{
    private bool hasKey = false;
    private ItemPanel itemPanel;
    private Canvas screenDark;

    public GameObject key;

    /// <summary>
    /// Checks if the player has the key to the basement and sets the key as inactive if so.
    /// </summary>
    void Start()
    {
        itemPanel = null;
        screenDark = null;
        CheckForItemPanel();
        CheckForScreenDark();

        StartCoroutine(WaitAndContinue());
    }

    IEnumerator WaitAndContinue()
    {
        yield return new WaitForSeconds(0.005f);

        if (PlayerSceneController.luzSotanoEncendida)
        {
            screenDark.gameObject.SetActive(false);
        }

        if (PlayerSceneController.congeladorPasado)
        {
            key.SetActive(false);
        }
    }

    /// <summary>
    /// Checks if the player has the key to the basement and sets the key as active if so.
    /// </summary>
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

    /// <summary>
    /// Checks if the player has the key in the inventory.
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

    /// <summary>
    /// This method is used to check if the screen is dark.
    /// </summary>
    void CheckForScreenDark()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
            {
                Canvas[] canvases = scene.GetRootGameObjects()
                                         .SelectMany(g => g.GetComponentsInChildren<Canvas>())
                                         .ToArray();

                Canvas screenDarkCanvas = canvases.FirstOrDefault(c => c.tag == "screenDark");

                if (screenDarkCanvas != null)
                {
                    screenDark = screenDarkCanvas;
                    Debug.Log("Screen dark found in scene: " + scene.name);
                    break;
                }
            }
        }

        if (screenDark == null)
        {
            Debug.LogWarning("No se encontró el tag 'screenDark' en las escenas cargadas.");
        }
    }
}

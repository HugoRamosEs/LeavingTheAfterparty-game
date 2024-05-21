using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script is used to control the behavior of the Tienda object.
/// </summary>
public class TiendaController : MonoBehaviour
{
    private bool hasKey = false;
    private ItemPanel itemPanel;
    private Image buffIcon;

    public GameObject tpToPlaya;
    public GameObject tiendaPanel;
    public DialogueGame dialogueGame;

    /// <summary>
    /// This methos is used to initialize the itemPanel and buffIcon variables.
    /// </summary>
    private void Start()
    {
        itemPanel = null;
        buffIcon = null;
        CheckForItemPanel();
        CheckForBuffIcon();
    }

    /// <summary>
    /// This method is used to check if the player has the key to enter the shop.
    /// </summary>
    /// <param name="collision"> Player's collision</param>
    /// <returns></returns>
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ItemSlot slot in itemPanel.inventory.slots)
            {
                if (slot.item != null && slot.item.Name == "Perla")
                {
                    hasKey = true;
                    break;
                }
            }

            if (!hasKey)
            {
                yield return new WaitForSecondsRealtime(0.80f);
                Time.timeScale = 0f;
                tiendaPanel.SetActive(true);
                dialogueGame.UpdateText("¡¡¡FUERA FUERA FUERA!!! Que estoy ocupado. No encuentro la maldita perla...");
                yield return new WaitForSecondsRealtime(4.75f);
                Time.timeScale = 1f;
                tpToPlaya.SetActive(true);
            }

            if (buffIcon.gameObject.activeSelf)
            {
                yield return new WaitForSecondsRealtime(0.80f);
                Time.timeScale = 0f;
                tiendaPanel.SetActive(true);
                dialogueGame.UpdateText("¡¿QUÉ QUIERES AHORA?! FUERA, no me molestes más...");
                yield return new WaitForSecondsRealtime(3.25f);
                Time.timeScale = 1f;
                tpToPlaya.SetActive(true);
            }
        }
    }

    /// <summary>
    /// This method is used to check if the player has the item required.
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
    /// This method is used to check the reference of the buffIcon.
    /// </summary>
    private void CheckForBuffIcon()
    {
        if (buffIcon == null)
        {
            Scene essentialScene = SceneManager.GetSceneByName("EsencialScene");

            if (essentialScene.IsValid())
            {
                GameObject[] objectsInScene = essentialScene.GetRootGameObjects();

                foreach (GameObject obj in objectsInScene)
                {
                    Canvas canvas = obj.GetComponentInChildren<Canvas>();

                    if (canvas != null)
                    {
                        Transform playerStatsPanel = canvas.transform.Find("PlayerStats");

                        if (playerStatsPanel != null)
                        {
                            Transform buffTransform = playerStatsPanel.Find("Buff");

                            if (buffTransform != null)
                            {
                                buffIcon = buffTransform.GetComponent<Image>();
                            }
                        }
                    }
                }
            }
        }
    }
}

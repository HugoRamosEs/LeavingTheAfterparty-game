using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TiendaController : MonoBehaviour
{
    private bool hasKey = false;
    private ItemPanel itemPanel;
    private Image buffIcon;

    public GameObject tpToPlaya;
    public GameObject tiendaPanel;
    public DialogueGame dialogueGame;


    private void Start()
    {
        itemPanel = null;
        buffIcon = null;
        CheckForItemPanel();
        CheckForBuffIcon();
    }

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

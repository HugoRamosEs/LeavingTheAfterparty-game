using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DonaController : MonoBehaviour
{
    private ItemPanel itemPanel;
    private bool hasSugarSack = false;

    public GameObject dialoguePanel;
    public DialogueGame dialogueGame;

    private void Start()
    {
        itemPanel = null;
        CheckForItemPanel();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ItemSlot slot in itemPanel.inventory.slots)
            {
                if (slot.item != null && slot.item.Name == "sack-sugar")
                {
                    hasSugarSack = true;
                    break;
                }
            }

            if (!hasSugarSack)
            {
                yield return new WaitForSecondsRealtime(0.80f);
                Time.timeScale = 0f;
                if (!dialoguePanel.activeInHierarchy)
                {
                    dialoguePanel.SetActive(true);
                }
                dialogueGame.UpdateText("Parece que necesitas un saco de azúcar...");
                yield return new WaitForSecondsRealtime(4.75f);
                Time.timeScale = 1f;
            }
            else
            {
                yield return new WaitForSecondsRealtime(0.80f);
                Time.timeScale = 0f;
                if (!dialoguePanel.activeInHierarchy)
                {
                    dialoguePanel.SetActive(true);
                }
                dialogueGame.UpdateText("¡Oh, veo que ya tienes un saco de azúcar!");
                yield return new WaitForSecondsRealtime(4.75f);
                Time.timeScale = 1f;
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
}
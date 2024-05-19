using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class DonaController : MonoBehaviour
{
    private bool hasSugarSack = false;
    private ItemPanel itemPanel;
    public GameObject tpToCiudad;
    public GameObject donaPanel;
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

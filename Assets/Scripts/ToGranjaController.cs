using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToGranjaController : MonoBehaviour
{
    private ItemPanel itemPanel;
    private PlayerSceneController player;
    private bool hasKey = false;

    public GameObject joaquin;
    public BoxCollider2D toGranjaCollider;
    public GameObject joaquinPanel;
    public Image joaquinImage;
    public DialogueGame dialogueGame;

    private void Start()
    {
        itemPanel = null;
        player = null;
        CheckForPlayerSceneController();
        CheckForItemPanel();

        if (player.playaPasada)
        {
            disableJoaquin();
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasKey = false;
            foreach (ItemSlot slot in itemPanel.inventory.slots)
            {
                if (slot.item != null && slot.item.Name == "Key")
                {
                    hasKey = true;
                    break;
                }
            }

            if (hasKey)
            {
                Time.timeScale = 0f;
                joaquinImage.enabled = true;
                joaquinPanel.SetActive(true);
                dialogueGame.UpdateText("Esa es mi llave?? Muchas gracias!!!! Me las piro vampiro (>_<)");

                float fadeDuration = 1f;
                float elapsedTime = 0f;
                Color startColor = joaquinImage.color;
                Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

                while (elapsedTime < fadeDuration)
                {
                    float t = elapsedTime / fadeDuration;
                    joaquinImage.color = Color.Lerp(startColor, targetColor, t);
                    elapsedTime += Time.unscaledDeltaTime;
                    yield return null;
                }
                joaquinImage.color = targetColor;

                yield return new WaitForSecondsRealtime(3.5f);
                disableJoaquin();
                player.playaPasada = true;
                joaquinPanel.SetActive(false);
                Time.timeScale = 1f;
                
                elapsedTime = 0f;
                while (elapsedTime < fadeDuration)
                {
                    float t = elapsedTime / fadeDuration;
                    joaquinImage.color = Color.Lerp(targetColor, startColor, t);
                    elapsedTime += Time.unscaledDeltaTime;
                    yield return null;
                }
                joaquinImage.color = startColor;
                
                joaquinImage.enabled = false;
            }
        }
    }

    private void disableJoaquin()
    {
        Destroy(joaquin);
        toGranjaCollider.enabled = false;
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
    
    private void CheckForPlayerSceneController()
    {
        Scene esencialScene = SceneManager.GetSceneByName("EsencialScene");

        if (esencialScene.IsValid())
        {
            GameObject[] objectsInScene = esencialScene.GetRootGameObjects();

            foreach (GameObject obj in objectsInScene)
            {
                PlayerSceneController foundPlayerSceneController = obj.GetComponentInChildren<PlayerSceneController>(true);

                if (foundPlayerSceneController != null)
                {
                    player = foundPlayerSceneController;
                    break;
                }
            }
        }
    }
}

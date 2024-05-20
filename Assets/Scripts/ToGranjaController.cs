using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used to handle the interaction between the player and the character Joaquin.
/// </summary>
public class ToGranjaController : MonoBehaviour
{
    private ItemPanel itemPanel;
    private bool hasKey = false;

    public GameObject joaquin;
    public BoxCollider2D toGranjaCollider;
    public GameObject joaquinPanel;
    public Image joaquinImage;
    public DialogueGame dialogueGame;

    /// <summary>
    /// This method is used to stablish som values at the start of the script execution depending on the state of the game.
    /// </summary>
    private void Start()
    {
        itemPanel = null;
        CheckForItemPanel();

        if (PlayerSceneController.playaPasada)
        {
            disableJoaquin();
        }
    }


    /// <summary>
    /// This method is used to handle the interaction between the player and the character Joaquin when the player enters the collider of the character.
    /// </summary>
    /// <param name="collision"> Player's collision</param>
    /// <returns></returns>
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasKey = false;
            foreach (ItemSlot slot in itemPanel.inventory.slots)
            {
                if (slot.item != null && slot.item.Name == "LlaveOxidada")
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
                dialogueGame.UpdateText("¿¿Esa es mi llave?? ¡¡Muchas gracias!! Me las piro vampiro.");

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
                PlayerSceneController.playaPasada = true;
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

    /// <summary>
    /// This method is used to disable the character Joaquin and the collider to the Granja scene.
    /// </summary>
    private void disableJoaquin()
    {
        Destroy(joaquin);
        toGranjaCollider.enabled = false;
    }

    /// <summary>
    /// This method is used to check for the reference of the ItemPanel in the EsencialScene.
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

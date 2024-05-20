using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// The script that handles the transformation of the boss.
/// </summary>
public class BarcoBossTransformation : MonoBehaviour
{
    private bool hasPerla = false;
    private bool hasLlave = false;
    private bool hasBotella = false;
    private Vector3 playerPosition;
    private ItemPanel itemPanel;
    private Transform playerTransform;

    public static bool KeepPlayerStill { get; set; } = false;
    public GameObject boss;
    public GameObject perla;
    public GameObject llave;
    public GameObject botella;
    public GameObject anciano;
    public GameObject barcoBoss;
    public GameObject sombraTransformacion;
    public GameObject sombraTransformacionReverse;
    public GameObject bloqueoTop;
    public GameObject muroNoHitbox;
    public GameObject colisionMuro;
    public DialogueGame dialogueGame;
    public Slider BossLifeBar;
    public ChangeSong audioBoss;
    public new BoxCollider2D collider;

    /// <summary>
    /// Check if the boss has been defeated.
    /// </summary>
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        itemPanel = null;
        CheckForItemPanel();
        CheckInventoryItems();

        if (PlayerSceneController.barcoBossPasado)
        {
            colisionMuro.SetActive(false);
            muroNoHitbox.SetActive(false);
            bloqueoTop.SetActive(false);
            anciano.SetActive(false);
            gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Check for the player's position.
    /// </summary>
    private void Update()
    {
        if (KeepPlayerStill)
        {
            playerTransform.position = playerPosition;
        }
    }
    /// <summary>
    /// Starts the boss transformation.
    /// </summary>
    /// <param name="collision"> The collision that starts the fight</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioBoss.ChangeBossSong();
            playerPosition = playerTransform.position;
            KeepPlayerStill = true;
            collider.size = new Vector2(collider.size.x * 1, collider.size.y * 1.35f);
            dialogueGame.gameObject.SetActive(true);
            dialogueGame.UpdateText("Ya... es... tarde...");
            StartCoroutine(ActivateBossAfterDelay(4f));
        }
    }

    /// <summary>
    /// Deactivates the boss transformation.
    /// </summary>
    /// <param name="other"> Check for the player tag </param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            barcoBoss.SetActive(false);
        }
    }

    /// <summary>
    /// Activates the boss after a delay.
    /// </summary>
    /// <param name="delay"> Time of delay </param>
    /// <returns></returns>
    private IEnumerator ActivateBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sombraTransformacion.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (anciano != null)
        {
            anciano.SetActive(false);
        }
        barcoBoss.SetActive(true);
        boss.SetActive(true);
        sombraTransformacion.SetActive(false);
        sombraTransformacionReverse.SetActive(true);
        yield return new WaitForSeconds(1f);
        sombraTransformacionReverse.SetActive(false);
        KeepPlayerStill = false;
        BossLifeBar.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Checks the inventory items.
    /// </summary>
    void CheckInventoryItems()
    {
        bool hasAllItems = false;

        foreach (ItemSlot slot in itemPanel.inventory.slots)
        {
            if (slot.item != null && slot.item.Name == "LlaveOxidada")
            {
                hasLlave = true;
            }
            else if (slot.item != null && slot.item.Name == "Perla")
            {
                hasPerla = true;
            }
            else if (slot.item != null && slot.item.Name == "broken_bottle")
            {
                hasBotella = true;
            }
        }

        if (hasPerla && hasLlave && hasBotella)
        {
            hasAllItems = true;
        }

        if (hasAllItems)
        {
            perla.SetActive(false);
            llave.SetActive(false);
            botella.SetActive(false);
        }
        else
        {
            if (hasPerla)
            {
                perla.SetActive(false);
            }
            else if (!hasPerla && PlayerSceneController.barcoBossPasado)
            {
                perla.SetActive(true);
            }

            if (hasLlave)
            {
                llave.SetActive(false);
            }
            else if (!hasLlave && PlayerSceneController.barcoBossPasado)
            {
                llave.SetActive(true);
            }

            if (hasBotella)
            {
                botella.SetActive(false);
            }
            else
            {
                botella.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Checks for the item panel.
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

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script manages the potion's behavior.
/// </summary>
public class Potion : MonoBehaviour
{
    private Image buffIcon;
    private Player player;
    private PlayerAttackMelee playerAttackMelee;
    private PlayerAttackShooting playerAttackShooting;

    /// <summary>
    /// The three types of potions.
    /// </summary>
    public enum PotionType
    {
        Health,
        Stamina,
        Damage
    }
    public PotionType potionType;
    public Sprite icon;
    public GameObject potion;
    public GameObject toPlaya;
    public GameObject dialogMark;

    private bool playerInRange;

    /// <summary>
    /// Check for the buff icon.
    /// </summary>
    private void Update()
    {
        if (buffIcon == null)
        {
            CheckForBuffIcon();
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ConsumePotion();
        }
    }

    /// <summary>
    /// Method to detect when the player enters the trigger area of the potion.
    /// </summary>
    /// <param name="collision"> Player's collision</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            dialogMark.SetActive(true);
            playerAttackMelee = collision.gameObject.GetComponent<PlayerAttackMelee>();
            playerAttackShooting = collision.gameObject.GetComponent<PlayerAttackShooting>();
            playerInRange = true;
        }
    }

    /// <summary>
    /// Method to detect when the player exits the trigger area of the potion.
    /// </summary>
    /// <param name="collision"> Player's collision</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
            dialogMark.SetActive(false);
        }
    }

    /// <summary>
    /// Method to consume the potion.
    /// </summary>
    private void ConsumePotion()
    {
        switch (potionType)
        {
            case PotionType.Health:
                player.hp.maxVal = 200;
                player.hp.SetToMax();
                player.UpdateHpBar();
                break;
            case PotionType.Stamina:
                player.stamina.maxVal = 400;
                player.stamina.SetToMax();
                player.UpdateStaminaBar();
                break;
            case PotionType.Damage:
                playerAttackMelee.damage = 20;
                playerAttackShooting.bullet.GetComponent<BulletScript>().damage = 35;
                break;
            default:
                break;
        }

        dialogMark.SetActive(true);
        buffIcon.gameObject.SetActive(true);
        buffIcon.sprite = icon;
        potion.SetActive(false);
        Destroy(gameObject);
        toPlaya.SetActive(true);
    }

    /// <summary>
    /// Method to check for the buff icon.
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

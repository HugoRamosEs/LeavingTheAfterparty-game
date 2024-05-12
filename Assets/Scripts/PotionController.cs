using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PotionController : MonoBehaviour
{
    private Image buffIcon;
    private Player player;
    private PlayerAttackMelee playerAttackMelee;
    private PlayerAttackShooting playerAttackShooting;

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
    public GameObject dialogueMark;

    private void Update()
    {
        if (buffIcon == null)
        {
            CheckForBuffIcon();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            playerAttackMelee = collision.gameObject.GetComponent<PlayerAttackMelee>();
            playerAttackShooting = collision.gameObject.GetComponent<PlayerAttackShooting>();
            dialogueMark.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
            dialogueMark.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player != null && Input.GetKeyDown(KeyCode.E))
            {
                ConsumePotion();
            }
        }
    }

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

        dialogueMark.SetActive(false);
        buffIcon.gameObject.SetActive(true);
        buffIcon.sprite = icon;
        potion.SetActive(false);
        Destroy(gameObject);
        toPlaya.SetActive(true);
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

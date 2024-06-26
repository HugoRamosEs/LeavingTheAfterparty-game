using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Class that handles the death screen.
/// </summary>
public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject hpBar;
    [SerializeField] private GameObject staminaBar;
    [SerializeField] private GameObject toolBarPanel;
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text deathCountText;
    [SerializeField] private Player2Movement player2Movement;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private PlayerAttackShooting playerAttackShooting;
    [SerializeField] private PlayerAttackMelee playerAttackMelee;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject configButton;
    [SerializeField] private GameObject lateralPanelButton;

    /// <summary>
    /// Sets up the death screen.
    /// </summary>
    public void Setup()
    {
        gameObject.SetActive(true);

        UltimoGuardado.Instance.IncrementDeathCount();

        audioSource.Play();

        int cantMuertes = UltimoGuardado.Instance.DeathCount;
        if (cantMuertes == 1)
        {
            deathCountText.text = UltimoGuardado.Instance.DeathCount + " muerte";
        } else
        {
            deathCountText.text = UltimoGuardado.Instance.DeathCount + " muertes";
        }
    }

    /// <summary>
    /// Respawns the player.
    /// </summary>
    public void ReaparecerBtn()
    {
        if (player == null)
        {
            Debug.LogError("Player is null");
            return;
        }

        player.gameObject.SetActive(true);

        player.transform.position = UltimoGuardado.Instance.PlayerPosition;

        hpBar.SetActive(true);
        staminaBar.SetActive(true);
        player.FullHeal();
        player.FullRest();
        player.isDead = false;
        player.isExhausted = false;
        player2Movement.isResting = false;
        playerAttackShooting.isShooting = false;
        playerAttackMelee.isAttacking = false;
        toolBarPanel.SetActive(true);
        inventoryButton.SetActive(true);
        configButton.SetActive(true);
        lateralPanelButton.SetActive(true);

        Time.timeScale = 1;

        Debug.Log("Reaparecer en la escena: " + UltimoGuardado.Instance.CurrentScene + " en la posición: " + UltimoGuardado.Instance.PlayerPosition);

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns to the main menu.
    /// </summary>
    public void VolverAlMenuBtn()
    {
        SceneManager.LoadScene("MenuPrincipalScene");
    }

}

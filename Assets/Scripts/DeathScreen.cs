using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject hpBar;
    [SerializeField] private GameObject staminaBar;
    [SerializeField] private GameObject toolBarPanel;
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text deathCountText;

    public void Setup()
    {
        gameObject.SetActive(true);

        UltimoGuardado.Instance.IncrementDeathCount();

        int cantMuertes = UltimoGuardado.Instance.DeathCount;
        if (cantMuertes == 1)
        {
            deathCountText.text = UltimoGuardado.Instance.DeathCount + " muerte";
        } else
        {
            deathCountText.text = UltimoGuardado.Instance.DeathCount + " muertes";
        }
    }

    public void ReaparecerBtn()
    {
        if (player == null)
        {
            Debug.LogError("Player is null");
            return;
        }

        player.gameObject.SetActive(true);
        player.FullHeal();
        player.FullRest();
        player.isDead = false;

        // recargar escena actual

        player.transform.position = UltimoGuardado.Instance.PlayerPosition;

        hpBar.SetActive(true);
        staminaBar.SetActive(true);
        toolBarPanel.SetActive(true);

        Time.timeScale = 1;

        Debug.Log("Reaparecer en la escena: " + UltimoGuardado.Instance.CurrentScene + " en la posición: " + UltimoGuardado.Instance.PlayerPosition);

        gameObject.SetActive(false);
    }

    public void VolverAlMenuBtn()
    {
        SceneManager.LoadScene("MenuPrincipalScene");
    }

}

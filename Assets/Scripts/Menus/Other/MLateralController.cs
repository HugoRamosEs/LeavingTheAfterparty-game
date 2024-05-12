using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MLateralController : MonoBehaviour
{
    [SerializeField] GameObject mLateralPanel;
    [SerializeField] GameObject img;
    [SerializeField] GameObject menuOpcions;
    [SerializeField] Button btnContinuar;
    private GameObject playerStats;
    string[] escenasConMenu = { "SotanoScene", "EsencialScene" };

    void Start()
    {
        btnContinuar.onClick.AddListener(OnContinueButtonClicked);
    }

    void OnContinueButtonClicked()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (playerStats == null)
        {
            CheckForPlayerStatsPanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (string escena in escenasConMenu)
            {
                if (SceneManager.GetActiveScene().name == escena)
                {
                    img.SetActive(!img.activeInHierarchy);
                    mLateralPanel.SetActive(!mLateralPanel.activeInHierarchy);
                    playerStats.SetActive(!playerStats.activeInHierarchy);
                    Time.timeScale = mLateralPanel.activeInHierarchy ? 0f : 1f;
                    break;
                }
            }
        }
    }

    private void CheckForPlayerStatsPanel()
    {
        if (playerStats == null)
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
                        Transform playerStatsPanelTransform = canvas.transform.Find("PlayerStats");

                        if (playerStatsPanelTransform != null)
                        {
                            playerStats = playerStatsPanelTransform.gameObject;
                        }
                    }
                }
            }
        }
    }
}


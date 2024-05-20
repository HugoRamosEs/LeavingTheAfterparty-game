using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script is to control the lateral panel
/// </summary>
public class MLateralController : MonoBehaviour
{
    [SerializeField] GameObject mLateralPanel;
    [SerializeField] GameObject img;
    [SerializeField] GameObject menuOpcions;
    [SerializeField] Button btnContinuar;
    private GameObject playerStats;
    string[] escenasConMenu = { "SotanoScene", "EsencialScene" };

    /// <summary>
    /// This method is used to ensure the functionality of the lateral panel
    /// </summary>
    void Start()
    {
        btnContinuar.onClick.AddListener(OnContinueButtonClicked);
    }

    /// <summary>
    /// This method is used to continue the game
    /// </summary>
    void OnContinueButtonClicked()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// This method is used to show the lateral panel
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (string escena in escenasConMenu)
            {
                if (SceneManager.GetActiveScene().name == escena)
                {
                    img.SetActive(!img.activeInHierarchy);
                    mLateralPanel.SetActive(!mLateralPanel.activeInHierarchy);
                    Time.timeScale = mLateralPanel.activeInHierarchy ? 0f : 1f;
                    break;
                }
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script is to control the lateral panel
/// </summary>
public class MLateralController : MonoBehaviour
{
    [SerializeField] GameObject mLateralPanel;
    [SerializeField] GameObject menuOpcions;
    [SerializeField] Button btnContinuar;
    string[] escenasSinMenu = { "LoginUserScene", "MenuPrincipalScene", "CasaScene" };

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
        if (Input.GetKeyDown(KeyCode.M))
        {
            bool isExcludedScene = false;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                foreach (string escena in escenasSinMenu)
                {
                    if (scene.name == escena)
                    {
                        isExcludedScene = true;
                        break;
                    }
                }
                if (isExcludedScene)
                {
                    break;
                }
            }

            if (!isExcludedScene)
            {
                mLateralPanel.SetActive(!mLateralPanel.activeInHierarchy);
                Time.timeScale = mLateralPanel.activeInHierarchy ? 0f : 1f;
            }
        }
    }
}

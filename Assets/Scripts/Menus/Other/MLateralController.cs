using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MLateralController : MonoBehaviour
{
    [SerializeField] GameObject mLateralPanel;
    [SerializeField] GameObject menuOpcions;
    [SerializeField] Button btnContinuar;
    string[] escenasSinMenu = { "LoginUserScene", "MenuPrincipalScene", "CasaScene" };

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

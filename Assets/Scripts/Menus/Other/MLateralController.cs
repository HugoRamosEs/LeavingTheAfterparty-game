using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

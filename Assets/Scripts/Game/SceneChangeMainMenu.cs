using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used to change the scene to the main menu.
/// </summary>
public class SceneChangeMainMenu : MonoBehaviour
{
    [SerializeField] string to;
    [SerializeField] string additionalScene;
    [SerializeField] Button button;

    /// <summary>
    /// This method adds a listener to the button for execute <see cref="SwitchScene"/> and turns timeScale to 1.
    /// </summary>
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(SwitchScene);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// This method is used to change the scene to the main menu.
    /// </summary>
    public void SwitchScene()
    {
        SceneManager.LoadScene(to);
        if (!string.IsNullOrEmpty(additionalScene))
        {
            SceneManager.LoadScene(additionalScene, LoadSceneMode.Additive);
        }
    }
}

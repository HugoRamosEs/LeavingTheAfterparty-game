using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeMainMenu : MonoBehaviour
{
    [SerializeField] string to;
    [SerializeField] string additionalScene;
    [SerializeField] Button button;
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(SwitchScene);
    }
    public void SwitchScene()
    {
        SceneManager.LoadScene(to);
        if (!string.IsNullOrEmpty(additionalScene))
        {
            SceneManager.LoadScene(additionalScene, LoadSceneMode.Additive);
        }
    }
}

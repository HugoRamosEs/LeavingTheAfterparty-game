using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsBack : MonoBehaviour
{
    public Button btnTornar;
    public GameObject panelInici;
    public GameObject panelLateral;
    public GameObject panelOptions;
    void Start()
    {
        btnTornar.onClick.AddListener(() => ActionButton());
    }
    void ActionButton()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MenuPrincipalScene")
        {
            panelInici.SetActive(true);
            panelOptions.SetActive(false);
        }
        else
        {
            panelLateral.SetActive(true);
            panelOptions.SetActive(false);
        }
    }
}

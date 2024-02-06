using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MLateralController : MonoBehaviour
{
    [SerializeField] GameObject mLateralPanel;
    [SerializeField] GameObject menuOpcions;
    bool comparador = false;
    string[] escenasConMenu = { "SotanoScene", "EsencialScene" };
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuOpcions.activeInHierarchy)
        {
            foreach (string escena in escenasConMenu)
            {
                if (SceneManager.GetActiveScene().name == escena)
                {
                    mLateralPanel.SetActive(!mLateralPanel.activeInHierarchy);
                    break;
                }
            }
        }

        comparador = menuOpcions.activeInHierarchy;
    }
}

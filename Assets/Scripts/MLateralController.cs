using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLateralController : MonoBehaviour
{
    [SerializeField] GameObject mLateralPanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mLateralPanel.SetActive(!mLateralPanel.activeInHierarchy);
        }
    }
}

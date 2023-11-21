using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsGraphics : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int quality;
    void Start()
    {
        quality = PlayerPrefs.GetInt("qualitat", 3);
        dropdown.value = quality;
        ChangeQuality();
    }
    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("qualitat", dropdown.value);
        quality = dropdown.value;
    }
}

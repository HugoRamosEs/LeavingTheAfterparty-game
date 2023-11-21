using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsBrightness : MonoBehaviour
{
    public Slider slider;
    public float sliderVal;
    public Image panelBrillo;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("luminositat", 0.5f);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);
    }
    public void ChangeSlider(float valor)
    {
        sliderVal = valor;
        PlayerPrefs.SetFloat("luminositat", sliderVal);
        PlayerPrefs.Save();
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);
    }
}

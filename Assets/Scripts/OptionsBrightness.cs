using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsBrightness : MonoBehaviour
{
    public Slider slider;
    public Image panelBrillo;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("luminositat", 0.5f);
        UpdateBrightness(1 - slider.value);
    }

    public void ChangeSlider(float valor)
    {
        PlayerPrefs.SetFloat("luminositat", valor);
        PlayerPrefs.Save();
        UpdateBrightness(1 - valor);
    }

    private void UpdateBrightness(float value)
    {
        float brightness = Mathf.Lerp(0.1f, 0.8f, value);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, brightness);
    }
}
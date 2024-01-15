using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsVolum : MonoBehaviour
{
    public Slider slider;
    public float sliderVal;
    public Image imgMute;
    public Image imgUnMute;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volum", 0.5f);
        AudioListener.volume = slider.value;
        Mute();
    }
    public void ChangeSlider(float valor)
    {
        sliderVal = valor;
        PlayerPrefs.SetFloat("volum", sliderVal);
        PlayerPrefs.Save();
        AudioListener.volume = slider.value;
        Mute();
    }
    public void Mute()
    {
        if (sliderVal == 0)
        {
            imgMute.enabled = true;
            imgUnMute.enabled = false;
        }
        else
        {
            imgMute.enabled = false;
            imgUnMute.enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to control the volume of the game
/// </summary>
public class OptionsVolum : MonoBehaviour
{
    public Slider slider;
    public float sliderVal;
    public Image imgMute;
    public Image imgUnMute;
   /// <summary>
   /// This method is used to set the volume of the game
   /// </summary>
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volum", 0.5f);
        AudioListener.volume = slider.value;
        Mute();
    }

    /// <summary>
    /// This method is used to change the volume of the game
    /// </summary>
    /// <param name="valor"> new value</param>
    public void ChangeSlider(float valor)
    {
        sliderVal = valor;
        PlayerPrefs.SetFloat("volum", sliderVal);
        PlayerPrefs.Save();
        AudioListener.volume = slider.value;
        Mute();
    }
    /// <summary>
    /// This method is to mute the game
    /// </summary>
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

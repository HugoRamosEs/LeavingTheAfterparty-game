using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class is used to manage the brightness slider in the options menu.
/// </summary>
public class OptionsBrightness : MonoBehaviour
{
    public Slider slider;
    public Image panelBrillo;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("luminositat", 0f);
        UpdateBrightness(1-slider.value);
    }
    /// <summary>
    /// The change of the slider value.
    /// </summary>
    /// <param name="valor"> The value for slider</param>
    public void ChangeSlider(float valor)
    {
        
        PlayerPrefs.SetFloat("luminositat", valor);
        PlayerPrefs.Save();
        UpdateBrightness(1 - valor);
    }
    /// <summary>
    /// The update of the brightness.
    /// </summary>
    /// <param name="value"> value of new brightness</param>
    private void UpdateBrightness(float value) {
        float brightness = Mathf.Lerp(0f, 0.75f,value);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b,brightness);
    }
}

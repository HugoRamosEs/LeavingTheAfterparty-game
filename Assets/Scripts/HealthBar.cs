using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform enemy;
    [SerializeField] private Vector3 offset;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;

        if (slider.value > 0.5f)
        {
            slider.fillRect.GetComponent<Image>().color = new Color32(106, 190, 48, 255);
        }
        else if (slider.value > 0.25f)
        {
            slider.fillRect.GetComponent<Image>().color = new Color(1, 0.64f, 0);
        }
        else
        {
            slider.fillRect.GetComponent<Image>().color = new Color32(204, 0, 0, 255);
        }
    }

    private void Update()
    {
        transform.position = enemy.position + offset;
    }
}
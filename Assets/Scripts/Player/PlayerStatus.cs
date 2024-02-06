using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Slider bar;

    public void Set(float current, float max)
    {
        bar.maxValue = max;
        bar.value = current;
        text.text = current.ToString() + "/" + max.ToString();
    }
}


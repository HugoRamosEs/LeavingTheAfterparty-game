using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to manage the fullscreen toggle in the options menu.
/// </summary>
public class OptionsFullscreen : MonoBehaviour
{
    public Toggle toogle;
    void Start()
    {
        if (Screen.fullScreen)
        {
            toogle.isOn = true;
        }
        else
        {
            toogle.isOn = false;
        }
    }
    public void ToogleFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}

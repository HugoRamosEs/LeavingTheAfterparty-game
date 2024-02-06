using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsFullscreen : MonoBehaviour
{
    public Toggle toogle;
    // Start is called before the first frame update
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

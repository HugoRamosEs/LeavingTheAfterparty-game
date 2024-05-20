using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is to load the options panel
/// </summary>
public class OptionsLoad : MonoBehaviour
{
    public OptionsController optionsPanel;
    // Start is called before the first frame update
    
    /// <summary>
    /// For ensure the functionality of the options panel
    /// </summary>
    void Start()
    {
        optionsPanel = GameObject.FindGameObjectWithTag("options").GetComponent<OptionsController>();
    }

    
    /// <summary>
    /// To show the options panel when the player press the scape key
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showOptions();
        }
    }

    /// <summary>
    /// Show the options panel
    /// </summary>
    public void showOptions()
    {
        optionsPanel.opcions.SetActive(true);
    }
}

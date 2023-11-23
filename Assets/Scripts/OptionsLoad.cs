using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsLoad : MonoBehaviour
{
    public OptionsController optionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        optionsPanel = GameObject.FindGameObjectWithTag("options").GetComponent<OptionsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showOptions();
        }
    }

    public void showOptions()
    {
        optionsPanel.opcions.SetActive(true);
    }
}

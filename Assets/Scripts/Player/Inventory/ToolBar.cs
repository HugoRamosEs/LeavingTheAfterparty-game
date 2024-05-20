using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to control the toolbar
/// </summary>
public class ToolBar : MonoBehaviour
{
    [SerializeField] int toolBarSize = 2;
    public int selectedTool;

    public Action<int> onChange;

    internal void Set(int id)
    {
        selectedTool = id;
    }
    void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            if (delta > 0)
            {
                selectedTool = (selectedTool + 1) % toolBarSize;
            }
            else
            {
                selectedTool = (selectedTool - 1 + toolBarSize) % toolBarSize;
            }
            onChange?.Invoke(selectedTool);
        }
    }
}

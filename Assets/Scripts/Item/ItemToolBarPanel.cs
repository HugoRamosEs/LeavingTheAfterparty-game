using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to manage the toolbar panel.
/// </summary>
public class ItemToolBarPanel : ItemPanel
{
    [SerializeField] ToolBar toolbar;

    /// <summary>
    /// This method is used to initialize the toolbar panel.
    /// </summary>
    void Start()
    {
        Init();
        toolbar.onChange += selectedItem;
        selectedItem(0);
    }
    /// <summary>
    /// This method is used to set the toolbar panel.
    /// </summary>
    /// <param name="id"> the id for the item to set</param>
    public override void OnClick(int id)
    {
        toolbar.Set(id);
        selectedItem(id);
    }

    int currentSelectedTool;

    /// <summary>
    /// This method is used to select an item.
    /// </summary>
    /// <param name="id">The item id</param>
    public void selectedItem(int id)
    {
        buttons[currentSelectedTool].toolSelected(false);
        currentSelectedTool = id;
        buttons[currentSelectedTool].toolSelected(true);
    }
}

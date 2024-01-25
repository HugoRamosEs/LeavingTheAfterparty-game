using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToolBarPanel : ItemPanel
{
    [SerializeField] ToolBar toolbar;
    void Start()
    {
        Init();
        toolbar.onChange += selectedItem;
        selectedItem(0);
    }
    public override void OnClick(int id)
    {
        toolbar.Set(id);
        selectedItem(id);
    }

    int currentSelectedTool;
    public void selectedItem(int id)
    {
        buttons[currentSelectedTool].toolSelected(false);
        currentSelectedTool = id;
        buttons[currentSelectedTool].toolSelected(true);
    }
}

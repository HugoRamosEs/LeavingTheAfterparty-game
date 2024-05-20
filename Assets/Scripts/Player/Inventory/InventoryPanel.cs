using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to control the inventory panel
/// </summary>
public class InventoryPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManager.instance.dragAndDrop.OnClick(inventory.slots[id]);
        Show();
    }
}

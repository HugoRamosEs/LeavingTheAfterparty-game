using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManager.instance.dragAndDrop.OnClick(inventory.slots[id]);
        Show();
    }
}

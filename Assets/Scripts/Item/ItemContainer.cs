using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
/// <summary>
/// This class is used to create item slots.
/// </summary>
public class ItemSlot
{
    public Item item;
    public int count;

    /// <summary>
    /// Copy the item slot.
    /// </summary>
    /// <param name="slot"> the slot</param>
    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

    /// <summary>
    /// Set the item and the count.
    /// </summary>
    /// <param name="item"> the item</param>
    /// <param name="count"> the quantity of the same item</param>
    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    /// <summary>
    /// Clear the item slot.
    /// </summary>
    public void Clear()
    {
        item = null;
        count = 0;
    }
}

[CreateAssetMenu(menuName = "Data/Item-Container")]
/// <summary>
/// This class is used to create item containers.
/// </summary>
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;
    public bool isDirty;

    public void Add(Item item, int count = 1)
    {
        isDirty = true;
        if (item.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            // Item no stackable
            ItemSlot itemSlot = slots.Find(x => x.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
            }
        }
    }
}

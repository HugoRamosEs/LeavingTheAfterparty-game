using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to manage the item panel.
/// </summary>
public class ItemPanel : MonoBehaviour
{
    public ItemContainer inventory;
    public List<InventoryButton> buttons;

    /// <summary>
    /// This method is used to initialize the item panel.
    /// </summary>
    void Start()
    {
        Init();
    }
    /// <summary>
    /// This method is used to call setIndex and show methods.
    /// </summary>
    public void Init()
    {
        SetIndex();
        Show();
    }
    /// <summary>
    /// This method is used to call the show method when enabled.
    /// </summary>
    private void OnEnable()
    {
        Show();
    }
    /// <summary>
    /// This method is used to set the index of the buttons.
    /// </summary>
    private void SetIndex()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }
    /// <summary>
    /// This method is used to call the show method when the inventory is dirty.
    /// </summary>
    public void LateUpdate()
    {
        if (inventory.isDirty)
        {
            Show();
            inventory.isDirty = false;
        }
    }

    /// <summary>
    /// This method is used to show the items in the inventory.
    /// </summary>
    public void Show()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            if (inventory.slots[i].item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }

    /// <summary>
    /// This method is used to call the OnClick method.
    /// </summary>
    /// <param name="id"> id of the clicked item</param>
    public virtual void OnClick(int id){}
}

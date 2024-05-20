using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// This script is used to control the inventory button
/// </summary>
public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text text;
    [SerializeField] Image selected;

    int myIndex;

    /// <summary>
    /// This method is used to set the index of the button
    /// </summary>
    /// <param name="index"> number of the index</param>
    public void SetIndex(int index)
    {
        myIndex = index;
    }

    /// <summary>
    /// This method is used to set the item of the button
    /// </summary>
    /// <param name="slot"> the item slot</param>
    public void Set(ItemSlot slot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        if (slot.item.stackable == true)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }  

    /// <summary>
    /// This method is used to clean the button
    /// </summary>
    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    /// <summary>
    /// This method is used to select the item
    /// </summary>
    /// <param name="eventData"> the pointer</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
        itemPanel.OnClick(myIndex);
    }
    /// <summary>
    /// This method is used to select the tool
    /// </summary>
    /// <param name="b"> boolean to change betweeen true and false.</param>
    public void toolSelected(bool b)
    {
        selected.gameObject.SetActive(b);
    }
}

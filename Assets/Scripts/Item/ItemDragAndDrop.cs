using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for dragging and dropping items from the inventory.
/// </summary>
public class ItemDragAndDrop : MonoBehaviour
{
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] GameObject itemIcon;
    public ItemContainer inventory;
    RectTransform iconTransform;
    Image itemIconImage;

    /// <summary>
    /// This method is called when the player clicks on an item in the inventory.
    /// </summary>
    /// <param name="itemSlot"> the slot clicked</param>
    internal void OnClick(ItemSlot itemSlot)
    {
        if (this.itemSlot == null)
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
            inventory.isDirty = true;
        }
        else
        {
            Item item = itemSlot.item;
            int count = itemSlot.count;
            itemSlot.Copy(this.itemSlot);
            this.itemSlot.Set(item, count);
            inventory.isDirty = true;
        }
        UpdateIcon();
    }

    /// <summary>
    /// This method is used to update the icon of the item being dragged.
    /// </summary>
    void UpdateIcon()
    {
        if(itemSlot.item == null)
        {
            itemIcon.SetActive(false);
        }
        else
        {
            itemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.item.icon;
        }
    }
    
    /// <summary>
    /// This method is used to start the item icon and set the item slot.
    /// </summary>
    void Start()
    {
        itemSlot = new ItemSlot();
        iconTransform = itemIcon.GetComponent<RectTransform>();
        itemIconImage = itemIcon.GetComponent<Image>();
    }

    /// <summary>
    /// This method is used to update the item icon position and check if the player has dropped the item.
    /// </summary>
    void Update()
    {
        if (itemIcon != null && itemIcon.activeInHierarchy == true)
        {
            iconTransform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    Debug.Log("Drop item");
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;
                    ItemSpawner.instance.Spawn(worldPosition, itemSlot.item, itemSlot.count);
                    itemSlot.Clear();
                    itemIcon.SetActive(false);
                }
            }
        }
    }
}

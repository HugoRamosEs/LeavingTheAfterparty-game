using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDragAndDrop : MonoBehaviour
{
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] GameObject itemIcon;
    public ItemContainer inventory;
    RectTransform iconTransform;
    Image itemIconImage;
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
    void Start()
    {
        itemSlot = new ItemSlot();
        iconTransform = itemIcon.GetComponent<RectTransform>();
        itemIconImage = itemIcon.GetComponent<Image>();
    }
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

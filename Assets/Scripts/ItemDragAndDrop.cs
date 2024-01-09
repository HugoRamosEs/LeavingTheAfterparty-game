using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrangAndDrop : MonoBehaviour
{
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] GameObject dragAndDrop;

    internal void OnClick(ItemSlot itemSlot)
    {
        if (this.itemSlot == null)
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
        }
        else
        {
            Item item = itemSlot.item;
            int count = itemSlot.count;
            itemSlot.Copy(this.itemSlot);
            this.itemSlot.Set(item, count);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        itemSlot = new ItemSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

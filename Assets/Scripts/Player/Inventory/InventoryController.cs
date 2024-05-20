using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is used to control the inventory of the player
/// </summary>
public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            panel.SetActive(!panel.activeInHierarchy);
        }
    }
}

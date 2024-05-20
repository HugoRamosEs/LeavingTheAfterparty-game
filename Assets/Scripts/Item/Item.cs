using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]

/// <summary>
/// This class is used to create items.
///</summary>

public class Item : ScriptableObject
{
    public string Name;
    public bool stackable;
    public Sprite icon;
}

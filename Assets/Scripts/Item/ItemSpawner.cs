using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns items in the world
/// </summary>
public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instance;
    void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject itemPrefab;
    public void Spawn(Vector3 position, Item item, int count)
    {
        GameObject o = Instantiate(itemPrefab, position, Quaternion.identity);
        o.GetComponent<ItemPickUp>().Set(item, count);
    }
}

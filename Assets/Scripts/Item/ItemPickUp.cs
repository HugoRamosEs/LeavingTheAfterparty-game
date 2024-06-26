using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to pick up items from the ground.
/// </summary>
public class ItemPickUp : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 2.5f;
    [SerializeField] float pickUpDistance = 1.5f;
    // [SerializeField] float ttl = 10f; SEGUNDOS PARA QUE EL ITEM DESAPARZCA DE LA ESCENA
    public Item item;
    public int count = 1;

    /// <summary>
    /// This method is used to check for the player.
    /// </summary>
    void Awake()
    {
        //player = GameManager.instance.player.transform;
        CheckForPlayerWithTag();
    }

    
    /// <summary>
    /// This method is used to set the item and the count.
    /// </summary>
    /// <param name="item"> the item</param>
    /// <param name="count"> the count</param>
    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
    }

    /// <summary>
    /// This method is used to update the item.
    /// </summary>
    void Update()
    {
        // ESTO SIRVE PARA QUE EL ITEM DESAPAREZCA DE LA ESCENA PASADOS LOS SEGUNDOS DE ARRIBA
        // ttl -= Time.deltaTime;
        // if (ttl < 0)
        // {
        // Destroy(gameObject);
        // }

        if (player == null)
        {
            CheckForPlayerWithTag();
            return;
        }
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickUpDistance)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (distance < 0.1f)
        {
            if (GameManager.instance.inventoryContainer != null)
            {
                GameManager.instance.inventoryContainer.Add(item, count);
            } 
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// This method is used to check for the player.
    /// </summary>
    void CheckForPlayerWithTag()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
}

using UnityEngine;

/// <summary>
/// This script is used to unlock the door when the player interacts with it.
/// </summary>
public class UnlockDonaScript : MonoBehaviour
{
    public GameObject hitbox;
    public GameObject muros;
    private bool playerInCollider = false;

    /// <summary>
    /// Check if the player is in the collider and if the player presses the F key, the door will be unlocked.
    /// </summary>
    void Update()
    {
        if (playerInCollider && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Player has interacted with the object");
            hitbox.SetActive(false);
            muros.SetActive(false);
        }
    }

    /// <summary>
    /// Check if the player is in the collider.
    /// </summary>
    /// <param name="collision"> Player's collider</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInCollider = true;
        }
    }

    /// <summary>
    /// Check if the player is not in the collider.
    /// </summary>
    /// <param name="collision"> Player's collider</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInCollider = false;
        }
    }
}

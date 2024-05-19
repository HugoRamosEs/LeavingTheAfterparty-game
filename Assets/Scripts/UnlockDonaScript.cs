using UnityEngine;

public class UnlockDonaScript : MonoBehaviour
{
    public GameObject hitbox;
    public GameObject muros;
    private bool playerInCollider = false;

    void Update()
    {
        if (playerInCollider && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Player has interacted with the object");
            hitbox.SetActive(false);
            muros.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInCollider = false;
        }
    }
}

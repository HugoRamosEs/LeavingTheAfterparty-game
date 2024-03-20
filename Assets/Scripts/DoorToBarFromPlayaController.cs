using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToBarFromPlayaController : MonoBehaviour
{
    public GameObject playaPanel;
    public DialogueGame dialogueGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playaPanel.SetActive(true);
            dialogueGame.UpdateText("Creo que no es muy buena idea volver al bar...");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playaPanel.SetActive(false);
        }
    }
}

using UnityEngine;

/// <summary>
/// This class is responsible for managing the door to bar from playa interaction.
/// </summary>
public class DoorToBarFromPlayaController : MonoBehaviour
{
    public GameObject playaPanel;
    public DialogueGame dialogueGame;

    /// <summary>
    ///  This method blocks the player from entering the bar from the beach.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playaPanel.SetActive(true);
            dialogueGame.UpdateText("Creo que no es muy buena idea volver al bar...");
        }
    }

    /// <summary>
    /// This method is called when the Player exits the trigger area.
    /// </summary>
    /// <param name="collision"> Player's collision</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playaPanel.SetActive(false);
        }
    }
}

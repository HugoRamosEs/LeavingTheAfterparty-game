using System.Collections;

using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Controller of the house, it shows a message when the player enters the house and then shows the final message.
/// </summary>
public class CasaController : MonoBehaviour
{
    public float duration = 2f;
    public GameObject canvasCasa;
    public DialogueGame dialogueGame;
    public GameObject canvasFinal;
    public Image image;
    public GameObject panelFinal;

    /// <summary>
    /// When the player enters the house, it shows a message and then the final message.
    /// </summary>
    /// <param name="collision"> the player's collision</param>
    /// <returns></returns>
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            yield return new WaitForSecondsRealtime(0.80f);
            Time.timeScale = 0f;
            canvasCasa.SetActive(true);
            dialogueGame.UpdateText("¡¡¿¿NO MIRAS EL MÓVIL O QUÉ??!! De nada te sirven los donuts... ¡SE TE VA A QUEDAR LA CHANCLA TATUADA!");
            yield return new WaitForSecondsRealtime(7.25f);
            canvasFinal.SetActive(true);
            StartCoroutine(FadeImage());
        }
    }
    /// <summary>
    /// Fade the image of the final panel.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeImage()
    {
        for (float t = 0.01f; t < duration; t += Time.unscaledDeltaTime)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, t / duration));
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        panelFinal.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);

        for (float t = 0.01f; t < duration; t += Time.unscaledDeltaTime)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1, 0, t / duration));
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        image.gameObject.SetActive(false);
    }
}

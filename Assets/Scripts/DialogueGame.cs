using System;
using System.Collections;
using TMPro;
using UnityEngine;
/// <summary>
/// This class is responsible for displaying the dialogue text on the screen.
/// </summary>
public class DialogueGame : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;

    /// <summary>
    /// This method is responsible for updating the text on the screen.
    /// </summary>
    /// <param name="message"> the message of the text </param>
    /// <returns></returns>
    public Coroutine UpdateText(string message)
    {
        text.text = "";
        typingCoroutine = StartCoroutine(TypeText(message));

        return typingCoroutine;
    }
    /// <summary>
    /// The method responsible for typing the text on the screen.
    /// </summary>
    /// <param name="message"> the message of the text </param>
    /// <returns></returns>
    private IEnumerator TypeText(string message)
    {
        foreach (char letter in message)
        {
            text.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }
    /// <summary>
    /// A method that stops the text from being typed on the screen.
    /// </summary>
    public void StopText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
    }
    /// <summary>
    /// A method that clears the text on the screen.
    /// </summary>
    public void ClearPanel()
    {

    }
}

using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueGame : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;

    public Coroutine UpdateText(string message)
    {
        text.text = "";
        typingCoroutine = StartCoroutine(TypeText(message));

        return typingCoroutine;
    }

    private IEnumerator TypeText(string message)
    {
        foreach (char letter in message)
        {
            text.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    public void StopText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
    }
}

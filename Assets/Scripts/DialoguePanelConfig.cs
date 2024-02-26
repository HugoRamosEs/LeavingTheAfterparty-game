using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class DialoguePanelConfig : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private Image characterImage;

    public float typingSpeed = 0.05f;
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void UpdateDialogue(string text, string name, Sprite image)
    {
        gameObject.SetActive(true);

        NameText.text = name;
        characterImage.sprite = image;
        dialogueText.text = ""; 

        StopAllCoroutines(); 
        StartCoroutine(TypeSentence(text));
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void ShowDialogue()
    {
        gameObject.SetActive(true);
    }

    public void HideDialogue()
    {
        StopAllCoroutines(); 
        dialogueText.text = "";
        gameObject.SetActive(false);
    }

}

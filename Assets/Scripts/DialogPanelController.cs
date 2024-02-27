using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogPanelController : MonoBehaviour
{
    private Transform player;
    private DialoguePanelConfig dialoguePanel;
    private bool dialogueShown = false;

    public float distance = 1f;
    public string npcMessage = "Press C to interact...";
    public string npcName = "Name...";
    public Sprite npcImage;


    void Update()
    {
        if (player == null)
        {
            CheckForPlayerWithTag(); 
        }

        if (dialoguePanel == null)
        {
            CheckForDialogPanelWithTag();
        }

        if (Vector3.Distance(transform.position, player.position) <= distance && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Interacting with NPC");
            if (dialoguePanel != null)
            {
                Debug.Log("Interacting with NP2C");
                ToggleDialogue(dialoguePanel);
            }
        }
    }

    void ToggleDialogue(DialoguePanelConfig dialoguePanel)
    {
        dialogueShown = !dialogueShown;
        if (dialogueShown)
        {
            dialoguePanel.gameObject.SetActive(true);
            dialoguePanel.UpdateDialogue(npcImage, npcName, npcMessage);
            dialogueShown = true;
        }
        else
        {
            dialoguePanel.HideDialogue();
        }
    }

    void CheckForPlayerWithTag()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void CheckForDialogPanelWithTag()
    {
        GameObject dialogPanelObject = GameObject.FindWithTag("DialogPanel");

        if (dialogPanelObject != null)
        {
            dialoguePanel = dialogPanelObject.GetComponent<DialoguePanelConfig>();
        }
    }
}

using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    Transform player;
    public float interactionDistance = 5f;
    public string interactionMessage = "La tecla C ha sido presionada enfrente del NPC";

    private DialoguePanelConfig dialoguePanelConfig;

    void Awake()
    {
        CheckForPlayerWithTag();
    }

    DialoguePanelConfig GetDialoguePanelConfig()
    {
        if (dialoguePanelConfig == null)
        {
            dialoguePanelConfig = FindObjectOfType<DialoguePanelConfig>();
        }
        return dialoguePanelConfig;
    }

    void Update()
    {
        if (player == null)
        {
            CheckForPlayerWithTag();
            return;
        }

        if (Vector3.Distance(transform.position, player.position) <= interactionDistance && Input.GetKeyDown(KeyCode.C))
        {
            DialoguePanelConfig config = GetDialoguePanelConfig();
            if (config != null)
            {
                config.UpdateDialogueText(interactionMessage);
            }
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
}

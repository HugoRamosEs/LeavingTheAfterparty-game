using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 5f;
    public string interactionMessage = "La tecla C ha sido presionada enfrente del NPC";
    public string npcName = "Nombre del NPC";
    public Sprite npcImage;
    private DialoguePanelConfig dialoguePanelConfig;
    private bool isDialogueShown = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dialoguePanelConfig = FindObjectOfType<DialoguePanelConfig>();
    }

    void Update()
    {
        if (player == null || dialoguePanelConfig == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, player.position) <= interactionDistance && Input.GetKeyDown(KeyCode.C))
        {
            ToggleDialogue();
        }
    }

    void ToggleDialogue()
    {
        isDialogueShown = !isDialogueShown;

        if (isDialogueShown)
        {
            dialoguePanelConfig.gameObject.SetActive(true);
            dialoguePanelConfig.UpdateDialogue(interactionMessage, npcName, npcImage);

        }
        else
        {
            dialoguePanelConfig.HideDialogue();
        }
    }


}

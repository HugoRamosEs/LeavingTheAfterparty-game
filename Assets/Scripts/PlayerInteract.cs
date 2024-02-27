using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar los eventos de cambio de escena.

public class NPCInteraction : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 5f;
    public string interactionMessage = "La tecla C ha sido presionada enfrente del NPC";
    public string npcName = "Nombre del NPC";
    public Sprite npcImage;
    private DialoguePanelConfig dialoguePanelConfig;
    private bool isDialogueShown = false;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        if (scene.name == "EsencialScene")
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null) Debug.LogError("Player not found!");

            dialoguePanelConfig = FindObjectOfType<DialoguePanelConfig>();
            if (dialoguePanelConfig == null) Debug.LogError("DialoguePanelConfig not found!");
        }

        isDialogueShown = false;
    }



    void Update()
    {
        if (player == null)
        {
            CheckForPlayerWithTag();
        }

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

    void CheckForPlayerWithTag()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
}

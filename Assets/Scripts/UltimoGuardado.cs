using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is used to save the player's position and the current scene when the player enters a new scene.
/// </summary>
public class UltimoGuardado : MonoBehaviour
{
    public static UltimoGuardado Instance { get; private set; }

    public Vector3 PlayerPosition { get; private set; }
    public string CurrentScene { get; private set; }
    public Player Player { get; private set; }
    public int DeathCount { get; private set; }

    /// <summary>
    /// This method is used to initialize the singleton instance of this class.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// This method is used to unsubscribe from the 'sceneLoaded' event when the object is destroyed.
    /// </summary>
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// This method is used to save the player's position and the current scene when the player enters a new scene.
    /// </summary>
    /// <param name="scene"> The current scene to save position</param>
    /// <param name="mode"> The save state</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene loadedScene = SceneManager.GetSceneAt(i);
            if (loadedScene.name != "EsencialScene")
            {
                UpdateCurrentScene(loadedScene.name);

                Player player = FindObjectOfType<Player>();

                if (player != null)
                {
                    UpdatePlayerPosition(player.transform.position);
                    Player = player;
                }

                Debug.Log("Escena cargada: " + CurrentScene + " Posición del jugador: " + PlayerPosition);

                return;
            }
        }
    }

    /// <summary>
    /// This method is used to increment the death count of the player.
    /// </summary>
    public void IncrementDeathCount()
    {
        DeathCount++;
    }

    /// <summary>
    /// This method is used to update the player's position.
    /// </summary>
    /// <param name="newPosition"> the new respawn position of the player</param>
    public void UpdatePlayerPosition(Vector3 newPosition)
    {
        PlayerPosition = newPosition;
    }

    /// <summary>
    /// This method is used to update the current scene.
    /// </summary>
    /// <param name="newScene"> the new scene where is going to be a save position</param>
    public void UpdateCurrentScene(string newScene)
    {
        CurrentScene = newScene;
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UltimoGuardado : MonoBehaviour
{
    public static UltimoGuardado Instance { get; private set; }

    public Vector3 PlayerPosition { get; private set; }
    public string CurrentScene { get; private set; }
    public Player Player { get; private set; }
    public int DeathCount { get; private set; }

    public GuardarPartida guardarPartida;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayedOnSceneLoaded(scene, mode));
    }

    private IEnumerator DelayedOnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene loadedScene = SceneManager.GetSceneAt(i);
            if (loadedScene.name != "EsencialScene")
            {
                UpdateCurrentScene(loadedScene.name);
                Player player = FindObjectOfType<Player>();

                if (player != null)
                {
                    Debug.Log("Posicion de spawn: " + player.transform.position);

                    UpdatePlayerPosition(player.transform.position);
                    Player = player;
                } else
                {
                    Debug.Log("Player es nulo");
                }

                Debug.Log("Escena cargada: " + CurrentScene + " Posición del jugador: " + PlayerPosition);
                yield break;
            }
        }
    }

    /// <summary>
    /// Increases the death count.
    /// </summary>
    public void IncrementDeathCount()
    {
        DeathCount++;
    }

    /// <summary>
    /// Updates the player's position.
    /// </summary>
    /// <param name="newPosition">The new position of the player.</param>
    public void UpdatePlayerPosition(Vector3 newPosition)
    {
        PlayerPosition = newPosition;
    }

    /// <summary>
    /// Updates the current scene and triggers the save if required.
    /// </summary>
    /// <param name="newScene">The name of the new scene.</param>
    public void UpdateCurrentScene(string newScene)
    {
        CurrentScene = newScene;
        if (guardarPartida != null && (UserGameInfo.Instance != null && UserGameInfo.Instance.email != null && UserGameInfo.Instance.email != ""))
        {
            guardarPartida.Guardar();
        }
    }
}
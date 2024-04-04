using UnityEngine;
using UnityEngine.SceneManagement;

public class UltimoGuardado : MonoBehaviour
{
    public static UltimoGuardado Instance { get; private set; }

    public Vector3 PlayerPosition { get; private set; }
    public string CurrentScene { get; private set; }
    public Player Player { get; private set; }
    public int DeathCount { get; private set; }

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

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Encuentra la escena que no es 'EsencialScene'
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene loadedScene = SceneManager.GetSceneAt(i);
            if (loadedScene.name != "EsencialScene")
            {
                // Actualiza la escena actual
                UpdateCurrentScene(loadedScene.name);

                // Encuentra al jugador en la escena cargada
                Player player = FindObjectOfType<Player>();

                // Si el jugador existe, actualiza la posición del jugador y la referencia al jugador
                if (player != null)
                {
                    UpdatePlayerPosition(player.transform.position);
                    Player = player;
                }

                Debug.Log("Escena cargada: " + CurrentScene + " Posición del jugador: " + PlayerPosition);

                // Sal de la función una vez que encuentres la escena que no es 'EsencialScene'
                return;
            }
        }
    }

    public void IncrementDeathCount()
    {
        DeathCount++;
    }

    public void UpdatePlayerPosition(Vector3 newPosition)
    {
        PlayerPosition = newPosition;
    }

    public void UpdateCurrentScene(string newScene)
    {
        CurrentScene = newScene;
    }
}
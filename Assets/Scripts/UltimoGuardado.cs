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
        yield return new WaitForSeconds(0.5f);

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
                yield break;
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
        if (guardarPartida != null && (UserGameInfo.Instance.email != null || UserGameInfo.Instance.email != ""))
        {
            guardarPartida.Guardar();
        }
    }

}
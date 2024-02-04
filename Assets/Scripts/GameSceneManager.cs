using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    void Awake()
    {
        instance = this;
    }

    [SerializeField] SceneTint sceneTint;
    string currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    public void InitSwitchScene(string to, Vector3 targetPosition, int orderInLayer)
    {
        StartCoroutine(Change(to, targetPosition, orderInLayer));
    }
    IEnumerator Change(string to, Vector3 targetPosition, int orderInLayer)
    {
        sceneTint.Tint();
        yield return new WaitForSeconds(1f / sceneTint.speed + 0.1f);
        SwitchScene(to, targetPosition, orderInLayer);
        yield return new WaitForEndOfFrame();
        sceneTint.UnTint();
    }
    public void SwitchScene(string to, Vector3 targetPosition, int orderInLayer)
    {
        SceneManager.LoadScene(to, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;
        Transform playerTransform = GameManager.instance.player.transform;
        CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();
        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(playerTransform, targetPosition - playerTransform.position);
        GameManager.instance.player.transform.position = new Vector3(targetPosition.x, targetPosition.y, playerTransform.position.z);
        playerTransform.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
    }
}

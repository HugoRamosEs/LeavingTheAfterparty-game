using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for managing the scene transitions.
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    /// <summary>
    /// This method is used to set the instance of the class.
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    [SerializeField] SceneTint sceneTint;
    string currentScene;
    /// <summary>
    /// This method is used to set the current scene.
    /// </summary>
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    /// <summary>
    /// This method is used to start the scene transition.
    /// </summary>
    /// <param name="to"> the next scene</param>
    /// <param name="targetPosition"> the target</param>
    /// <param name="orderInLayer"> the new order in layer</param>
    public void InitSwitchScene(string to, Vector3 targetPosition, int orderInLayer)
    {
        StartCoroutine(Change(to, targetPosition, orderInLayer));
    }
    /// <summary>
    /// This method is used to change the scene.
    /// </summary>
    /// <param name="to"> the next scene</param>
    /// <param name="targetPosition"> the target</param>
    /// <param name="orderInLayer"> the new order in layer</param>
    /// <returns></returns>
    IEnumerator Change(string to, Vector3 targetPosition, int orderInLayer)
    {
        sceneTint.Tint();
        yield return new WaitForSeconds(1f / sceneTint.speed + 0.1f);
        SwitchScene(to, targetPosition, orderInLayer);
        yield return new WaitForEndOfFrame();
        sceneTint.UnTint();
    }

    /// <summary>
    /// This method is used to switch the scene.
    /// </summary>
    /// <param name="to"> the next scene</param>
    /// <param name="targetPosition"> the target</param>
    /// <param name="orderInLayer"> the new order in layer</param>
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

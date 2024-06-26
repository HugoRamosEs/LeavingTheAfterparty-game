using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ChangeType
{
    Teleport, Scene
}

/// <summary>
/// This class is used to change the scene or teleport the player to another location.
/// </summary>
public class SceneChange : MonoBehaviour
{
    [SerializeField] ChangeType changeType;
    [SerializeField] string sceneToChange;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] int orderInLayer;
    Transform destination;
    void Start()
    {
        destination = transform.GetChild(1);
    }
    public void InitChange(Transform toChange)
    {
        switch (changeType)
        {
            case ChangeType.Teleport:
                Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();
                currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toChange, destination.position - toChange.position);
                toChange.position = new Vector3(destination.position.x, destination.position.y, toChange.position.z);
                toChange.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
                break;
            case ChangeType.Scene:
                GameSceneManager.instance.InitSwitchScene(sceneToChange, targetPosition, orderInLayer);
                break;
        }
       
    }
}

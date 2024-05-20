using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This class is used to manage the options between scenes.
/// </summary>
public class OptionsBetweenScenes : MonoBehaviour
{
    private void Awake()
    {
        var entreEscenesArray = FindObjectsOfType<OptionsBetweenScenes>();
        if (entreEscenesArray.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}

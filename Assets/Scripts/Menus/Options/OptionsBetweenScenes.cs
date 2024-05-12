using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

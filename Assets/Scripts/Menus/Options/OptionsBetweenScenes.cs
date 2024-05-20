using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OptionsBetweenScenes : MonoBehaviour
{
    private static HashSet<string> uniqueIds = new HashSet<string>();
    public string uniqueId;

    private void Awake()
    {
        if (uniqueIds.Contains(uniqueId))
        {
            Destroy(gameObject);
            return;
        }

        uniqueIds.Add(uniqueId);
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        uniqueIds.Remove(uniqueId);
    }
}

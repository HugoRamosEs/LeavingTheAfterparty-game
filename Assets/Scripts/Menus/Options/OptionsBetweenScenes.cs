using System.Collections.Generic;
using UnityEngine;

public class OptionsBetweenScenes : MonoBehaviour
{
    private static List<OptionsBetweenScenes> instances = new List<OptionsBetweenScenes>();

    private void Awake()
    {
        if (!instances.Contains(this))
        {
            instances.Add(this);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instances.IndexOf(this) != 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        instances.Remove(this);
    }
}

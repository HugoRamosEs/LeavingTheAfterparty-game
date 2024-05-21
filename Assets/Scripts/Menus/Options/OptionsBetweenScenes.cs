using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to manage the options between scenes.
/// </summary>
public class OptionsBetweenScenes : MonoBehaviour
{
    private static List<OptionsBetweenScenes> instances = new List<OptionsBetweenScenes>();

    /// <summary>
    /// This method is called when the script instance is being loaded.
    /// </summary>
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

    /// <summary>
    /// This method is called when the object becomes enabled and active.
    /// </summary>
    private void OnDestroy()
    {
        instances.Remove(this);
    }
}

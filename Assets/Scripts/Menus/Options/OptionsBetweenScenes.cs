using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to manage the options between scenes.
/// </summary>
public class OptionsBetweenScenes : MonoBehaviour
{
    private static List<OptionsBetweenScenes> instances = new List<OptionsBetweenScenes>();

    [SerializeField]
    private string uniqueID; // An identifier to compare objects

    /// <summary>
    /// This method is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // Check if an instance with the same uniqueID already exists
        foreach (var instance in instances)
        {
            if (instance.uniqueID == this.uniqueID)
            {
                Destroy(gameObject); // Destroy the duplicate object
                return;
            }
        }

        // If no duplicate is found, add this instance to the list
        instances.Add(this);
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// This method is called when the object becomes enabled and active.
    /// </summary>
    private void OnDestroy()
    {
        instances.Remove(this);
    }
}

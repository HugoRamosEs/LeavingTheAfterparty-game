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

/*
 
using UnityEngine;

public class OptionsBetweenScenes : MonoBehaviour
{
    private static OptionsBetweenScenes _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Transferir los hijos de la escena 2 que estén en BetweenScenes al objeto DontDestroyOnLoad
            Transform[] children = transform.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in children)
            {
                if (child != transform && child.gameObject.scene.name == "Scene2")
                {
                    child.SetParent(_instance.transform);
                }
            }

            Destroy(gameObject);
        }
    }
}


*/

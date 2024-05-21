using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows you to activate menus found in other scenes.
/// </summary>
public class MLateralOpenMenu : MonoBehaviour
{
    private GameObject targetObject;

    public string objectToFind;

    /// <summary>
    /// Searches for the object to activate in the scene "BetweenScenes".
    /// </summary>
    void Start()
    {
        CheckForObjectInDontDestroyOnLoad();

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ToggleObject);
        }
        else
        {
            Debug.LogError("Este script necesita estar adjunto a un objeto con un componente Button.");
        }
    }

    /// <summary>
    /// Activates the object if it is not null.
    /// </summary>
    void ToggleObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
        else
        {
            Debug.LogError("El objeto a activar no ha sido encontrado o no está asignado.");
        }
    }

    /// <summary>
    /// Searches for the object in the "scene" "DontDestroyOnLoad".
    /// </summary>
    private void CheckForObjectInDontDestroyOnLoad()
    {
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            if (obj.scene.buildIndex == -1)
            {
                if (obj.name == "BetweenScenes")
                {
                    Transform canvasTransform = obj.transform.Find("Canvas");
                    if (canvasTransform != null)
                    {
                        Transform objectTransform = canvasTransform.Find(objectToFind);
                        if (objectTransform != null)
                        {
                            targetObject = objectTransform.gameObject;
                            Debug.Log("Objeto encontrado: " + objectToFind);
                            break;
                        }
                    }
                }
            }
        }

        if (targetObject == null)
        {
            Debug.LogError("No se encontró el objeto en la ruta especificada: " + objectToFind);
        }
    }
}

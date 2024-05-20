using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// This script is used to change the input of the game
/// </summary>
public class ChangeInput : MonoBehaviour {
    EventSystem system;
    public Selectable firstInput;

    /// <summary>
    /// This method is used to ensure the functionality of the input
    /// </summary>
    void Start()
    {
        system = EventSystem.current;
        if (firstInput != null)
        {
            firstInput.Select();
        }
    }

    /// <summary>
    /// This method is used to change the input of the game
    /// </summary>
    void Update() {
        if (system.currentSelectedGameObject != null) {
            if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift)) {
                Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                if (previous != null) {
                    previous.Select();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Tab)) {
                Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                if (next != null)  {
                    next.Select();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))  {
                Button button = system.currentSelectedGameObject.GetComponent<Button>();
                if (button != null)  {
                    button.onClick.Invoke();
                }
            }
        }
    }
}

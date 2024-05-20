using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This script is used to control the light in the "Sotano Bar" scene.
/// </summary>
public class LightController : MonoBehaviour
{
    private Canvas screenDark;
    private bool isPlayerInTrigger = false;
    public bool isDark = true;
    public GameObject dialogueMark;

    /// <summary>
    /// This method is used to controle the dark screen.
    /// </summary>
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "EsencialScene")
        {
            isDark = false;
        }
        else
        {
            isDark = true;
        }
    }
    /// <summary>
    /// This method is used to controle the dark screen and toggle it off.
    /// </summary>
    void Update()
    {
        if (screenDark == null)
        {
            CheckForScreenDark();
        }

        if (screenDark && isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            screenDark.gameObject.SetActive(true);
            dialogueMark.SetActive(false);
            Destroy(screenDark.gameObject);
            isDark = false;
        }
    }

    /// <summary>
    /// This method is used to check if the player is in the trigger.
    /// </summary>
    /// <param name="collision"> Player's collision</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isDark)
            {
                dialogueMark.SetActive(true);
            }
            isPlayerInTrigger = true;
        }
    }
    /// <summary>
    /// This method is used to check if the player is out of the trigger.
    /// </summary>
    /// <param name="collision"> Player's collision</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            dialogueMark.SetActive(false);
            isPlayerInTrigger = false;
        }
    }

    /// <summary>
    /// This method is used to check if the screen is dark.
    /// </summary>
    void CheckForScreenDark()
    {
        GameObject screenDarkObject = GameObject.FindWithTag("screenDark");

        if (screenDarkObject != null)
        {
            Canvas screenDarkCanvas = screenDarkObject.GetComponent<Canvas>();
            if (screenDarkCanvas != null)
            {
                screenDark = screenDarkCanvas;
            }
        }
    }
}

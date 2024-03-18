using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightController : MonoBehaviour
{
    private Canvas screenDark;
    private bool isPlayerInTrigger = false;
    public bool isDark = true;
    public GameObject dialogueMark;

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

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            dialogueMark.SetActive(false);
            isPlayerInTrigger = false;
        }
    }

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

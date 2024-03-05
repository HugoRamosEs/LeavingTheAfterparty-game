using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightController : MonoBehaviour
{
    private Canvas screenDark;
    private bool isPlayerInTrigger = false;
    public bool isDark = true;

    void Update()
    {
        if (screenDark == null)
        {
            CheckForScreenDark();
        }

        if (screenDark && isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            screenDark.gameObject.SetActive(true);
            Destroy(screenDark.gameObject);
            isDark = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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

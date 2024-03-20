using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGranjaController : MonoBehaviour
{
    private ItemPanel itemPanel;
    private SceneTint sceneTint;

    public GameObject joaquin;
    public BoxCollider2D toGranjaCollider;
    public bool hasKey = false;

    private void Start()
    {
        itemPanel = null;
        sceneTint = null;
        CheckForItemPanel();
        CheckForSceneTint();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasKey = false;
            foreach (ItemSlot slot in itemPanel.inventory.slots)
            {
                if (slot.item != null && slot.item.Name == "Key")
                {
                    hasKey = true;
                    break;
                }
            }

            if (hasKey)
            {
                Debug.Log("Player has key");
                sceneTint.Tint();
                yield return new WaitForSeconds(1f / sceneTint.speed + 0.1f);
                disableJoaquin();
                yield return new WaitForEndOfFrame();
                sceneTint.UnTint();
            }
        }
    }

    private void disableJoaquin()
    {
        if (hasKey)
        {
            joaquin.SetActive(false);
            toGranjaCollider.enabled = false;
        }
    }

    private void CheckForItemPanel()
    {
        Scene esencialScene = SceneManager.GetSceneByName("EsencialScene");

        if (esencialScene.IsValid())
        {
            GameObject[] objectsInScene = esencialScene.GetRootGameObjects();

            foreach (GameObject obj in objectsInScene)
            {
                ItemPanel foundItemPanel = obj.GetComponentInChildren<ItemPanel>(true);

                if (foundItemPanel != null)
                {
                    itemPanel = foundItemPanel;
                    break;
                }
            }
        }
    }

    private void CheckForSceneTint()
    {
        Scene esencialScene = SceneManager.GetSceneByName("EsencialScene");

        if (esencialScene.IsValid())
        {
            GameObject[] objectsInScene = esencialScene.GetRootGameObjects();

            foreach (GameObject obj in objectsInScene)
            {
                SceneTint foundSceneTint = obj.GetComponentInChildren<SceneTint>(true);

                if (foundSceneTint != null)
                {
                    sceneTint = foundSceneTint;
                    break;
                }
            }
        }
    }   
}

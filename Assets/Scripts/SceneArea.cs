using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneArea : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            transform.parent.GetComponent<SceneChange>().InitChange(collision.transform);
        }
    }
}

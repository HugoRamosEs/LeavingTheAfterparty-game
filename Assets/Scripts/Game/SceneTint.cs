using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to tint the screen with a color.
/// </summary>
public class SceneTint : MonoBehaviour
{
    [SerializeField] Color unTintedColor;
    [SerializeField] Color tintedColor;
    public float speed = 0.5f;
    Image image;
    float f;

    /// <summary>
    /// Get the item used to tint the screen 
    /// </summary>
    void Awake()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// Tint the screen with the tinted color
    /// </summary>
    public void Tint()
    {
        StopAllCoroutines();
        f = 0f;
        StartCoroutine(TintScreen());
    }
    /// <summary>
    /// Untint the screen.
    /// </summary>
    public void UnTint()
    {
        StopAllCoroutines();
        f = 0f;
        StartCoroutine(UnTintScreen());
    }

    /// <summary>
    /// Coroutine to tint the screen
    /// </summary>
    /// <returns></returns>
    private IEnumerator TintScreen()
    {
        while (f < 1f)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp(f, 0, 1f);
            Color c = image.color;
            c = Color.Lerp(unTintedColor, tintedColor, f);
            image.color = c;
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// Coroutine to untint the screen
    /// </summary>
    /// <returns></returns>
    private IEnumerator UnTintScreen()
    {
        while (f < 1f)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp(f, 0, 1f);
            Color c = image.color;
            c = Color.Lerp(tintedColor, unTintedColor, f);
            image.color = c;
            yield return new WaitForEndOfFrame();
        }
    }
}

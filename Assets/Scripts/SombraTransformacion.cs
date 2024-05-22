using System.Collections;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to play the animation of the boss transformation at the start of the fight.
/// </summary>
public class SombraTransformacion : MonoBehaviour
{
    public GameObject sombraTransformacion;
    public GameObject sombraTransformacionReverse;
    public Slider BossLifeBar;

    /// <summary>
    /// Starts the animation of the boss transformation.
    /// </summary>
    void OnEnable()
    {
        sombraTransformacion.SetActive(true);
        StartCoroutine(PlayReverseAnimationAfterDelay(1f));
    }

    /// <summary>
    /// Plays the reverse animation of the boss transformation after a delay.
    /// </summary>
    /// <param name="delay"> time of delay before the reverse animation</param>
    /// <returns></returns>
    private IEnumerator PlayReverseAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        sombraTransformacion.SetActive(false);
        BossLifeBar.gameObject.SetActive(true);
        sombraTransformacionReverse.SetActive(true);
        StartCoroutine(DestroyAfterDelay(1f));
    }

    /// <summary>
    /// Destroys the game object after a delay.
    /// </summary>
    /// <param name="delay"> Time of delay after destroy the game object</param>
    /// <returns></returns>
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        sombraTransformacionReverse.SetActive(false);

        Destroy(gameObject);
    }
}

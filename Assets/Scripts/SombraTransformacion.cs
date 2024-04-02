using System.Collections;
using UnityEngine;

public class SombraTransformacion : MonoBehaviour
{
    public GameObject sombraTransformacion;
    public GameObject sombraTransformacionReverse;

    void OnEnable()
    {
        sombraTransformacion.SetActive(true);
        StartCoroutine(PlayReverseAnimationAfterDelay(1f));
    }

    private IEnumerator PlayReverseAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        sombraTransformacion.SetActive(false);

        sombraTransformacionReverse.SetActive(true);
        StartCoroutine(DestroyAfterDelay(1f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        sombraTransformacionReverse.SetActive(false);

        Destroy(gameObject);
    }
}
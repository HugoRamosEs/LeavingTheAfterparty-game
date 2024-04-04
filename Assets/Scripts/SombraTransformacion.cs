using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class SombraTransformacion : MonoBehaviour
{
    public GameObject sombraTransformacion;
    public GameObject sombraTransformacionReverse;
    public Slider BossLifeBar;

    void OnEnable()
    {
        sombraTransformacion.SetActive(true);
        StartCoroutine(PlayReverseAnimationAfterDelay(1f));
    }

    private IEnumerator PlayReverseAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        sombraTransformacion.SetActive(false);
        BossLifeBar.gameObject.SetActive(true);
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
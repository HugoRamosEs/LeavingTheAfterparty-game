using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarcoBossTransformation : MonoBehaviour
{
    public GameObject barcoBoss;
    public DialogueGame dialogueGame;
    public GameObject anciano;
    public GameObject boss;
    public GameObject sombraTransformacion;
    public GameObject sombraTransformacionReverse;
    public Transform playerTransform;
    private Vector3 playerPosition;
    public static bool KeepPlayerStill { get; set; } = false;
    public Slider BossLifeBar;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (KeepPlayerStill)
        {
            playerTransform.position = playerPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerPosition = playerTransform.position;
            KeepPlayerStill = true;
            dialogueGame.gameObject.SetActive(true);
            dialogueGame.UpdateText("Ya... Es... Tarde...");
            StartCoroutine(ActivateBossAfterDelay(4f));
        }
    }

    private IEnumerator ActivateBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sombraTransformacion.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (anciano != null)
        {
            anciano.SetActive(false);
        }
        barcoBoss.SetActive(true);
        boss.SetActive(true);
        sombraTransformacion.SetActive(false);
        sombraTransformacionReverse.SetActive(true);
        yield return new WaitForSeconds(1f);
        sombraTransformacionReverse.SetActive(false);
        KeepPlayerStill = false;
        BossLifeBar.gameObject.SetActive(true);
        Destroy(gameObject);

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            barcoBoss.SetActive(false);
        }
    }
}
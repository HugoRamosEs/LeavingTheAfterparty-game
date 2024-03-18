using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BarcoBossFight : MonoBehaviour
{
    public Transform player;
    public int bossHealth = 100;
    public GameObject panelTransition;
    public TextMeshProUGUI textTransition;
    private Animator animator;
    private int MaxHealth;
    private bool step2 = false;
    private bool step3 = false;
    public GameObject magicBulletBossPrefab;
    public GameObject HeiserBossPrefab;

    void Start()
    {
        animator = GetComponent<Animator>();
        MaxHealth = bossHealth;
        InvokeRepeating("FireMagicBullet", 5f, 5f);
        InvokeRepeating("SpawnHeisers",8f,8f);
    }

    void Update()
    {
        if (player == null)
        {
            CheckForPlayerWithTag();
        }
        animator.SetInteger("Health", bossHealth);

        if (bossHealth <= (MaxHealth) * 0.66 && step2 == false)
        {
            CancelInvoke("FireMagicBullet");
            CancelInvoke("SpawnHeisers");
            step2 = true;
            StartCoroutine(PhaseTransition("El aire es m�s fr�o a tu alrededor..."));
            InvokeRepeating("FireMagicBullet", 13f, 13f);
            InvokeRepeating("SpawnHeisers", 2f, 2f);
        }
        if (bossHealth <= (MaxHealth) * 0.33 && step3 == false)
        {
            CancelInvoke("FireMagicBullet");
            CancelInvoke("SpawnHeisers");
            step3 = true;
            StartCoroutine(PhaseTransition("Se avecina un infierno inminente..."));
            InvokeRepeating("FireMagicBullet", 1f, 1f);
        }
        if (bossHealth <= 0)
        {
            CancelInvoke("FireMagicBullet");
            CancelInvoke("SpawnHeisers");
            StartCoroutine(PhaseTransition("�Que ha sido eso?", true));
        }
    }

    IEnumerator PhaseTransition(string phaseText,bool isBossDead = false)
    {
        float transitionTime = 1f;
        float panelTransitionTime = 0.1f;

        Color panelColor = panelTransition.GetComponent<Image>().color;
        for (float t = 0; t < panelTransitionTime; t += Time.unscaledDeltaTime)
        {
            panelColor.a = (t / panelTransitionTime) *1.2f;
            panelTransition.GetComponent<Image>().color = panelColor;
            yield return null;
        }

        textTransition.text = phaseText;
        Color textColor = textTransition.color;
        for (float t = 0; t < transitionTime; t += Time.unscaledDeltaTime)
        {
            textColor.a = t / transitionTime;
            textTransition.color = textColor;
            yield return null;
        }
        if (isBossDead)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = 0; // Set alpha to 0
                spriteRenderer.color = color;
            }

        }
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(2f);

        for (float t = 0; t < transitionTime; t += Time.unscaledDeltaTime)
        {
            textColor.a = 1 - (t / transitionTime);
            textTransition.color = textColor;
            yield return null;
        }

        for (float t = 0; t < panelTransitionTime; t += Time.unscaledDeltaTime)
        {
            panelColor.a = 1 - (t / panelTransitionTime);
            panelTransition.GetComponent<Image>().color = panelColor;
            yield return null;
        }


        Time.timeScale = 1;
        if (isBossDead)
        {
            Destroy(gameObject);
        }
    }
    void FireMagicBullet()
    {
        
            float radius = 10f;  
            Vector2 spawnPoint = (Vector2)player.position + Random.insideUnitCircle * radius;

            
            GameObject bullet = Instantiate(magicBulletBossPrefab, spawnPoint, Quaternion.identity);
        
    }
    void SpawnHeisers()
    {
        
            float radius = 3f;
            for (int i = 0; i < 2; i++)
            {
                Vector2 spawnPoint = (Vector2)player.position + Random.insideUnitCircle * radius;
                Instantiate(HeiserBossPrefab, spawnPoint, Quaternion.identity);
            }
        
    }

    void CheckForPlayerWithTag()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bossHealth -= 10;
        }
    }
}
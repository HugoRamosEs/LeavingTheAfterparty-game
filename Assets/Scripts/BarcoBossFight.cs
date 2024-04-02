using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using SuperTiled2Unity;

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
    private bool invulnerable = false;
    public GameObject magicBulletBossPrefab;
    public GameObject flameBallBossPrefab;
    public Slider BossLifeBar;
    public GameObject colisionMuro;
    public GameObject muroNoHitbox;

    void Start()
    {
        animator = GetComponent<Animator>();
        MaxHealth = bossHealth;
        InvokeRepeating("FireMagicBullet", 5f, 5f);
        InvokeRepeating("SpawnFlame",8f,8f);
        BossLifeBar.maxValue = bossHealth;
        BossLifeBar.value = bossHealth;
        colisionMuro.SetActive(true);
        muroNoHitbox.SetActive(true);
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
            CancelInvoke("SpawnFlame");
            step2 = true;
            StartCoroutine(PhaseTransition("El aire es más frío a tu alrededor..."));
            InvokeRepeating("FireMagicBullet", 13f, 13f);
            InvokeRepeating("SpawnFlame", 2f, 2f);
        }
        if (bossHealth <= (MaxHealth) * 0.33 && step3 == false)
        {
            CancelInvoke("FireMagicBullet");
            CancelInvoke("SpawnFlame");
            step3 = true;
            StartCoroutine(PhaseTransition("Se avecina un infierno inminente..."));
            InvokeRepeating("FireMagicBullet", 1f, 1f);
        }
        if (bossHealth <= 0)
        {
            CancelInvoke("FireMagicBullet");
            CancelInvoke("SpawnFlame");
            StartCoroutine(PhaseTransition("¿Que ha sido eso?", true));
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
                color.a = 0;
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
            colisionMuro.SetActive(false);
            muroNoHitbox.SetActive(false);
            Destroy(gameObject);
        }
    }
    void FireMagicBullet()
    {
        
            float radius = 10f;  
            Vector2 spawnPoint = (Vector2)player.position + Random.insideUnitCircle * radius;

            
            GameObject bullet = Instantiate(magicBulletBossPrefab, spawnPoint, Quaternion.identity);

            StartCoroutine(DelayBullet(bullet));

    }
    IEnumerator DelayBullet(GameObject bullet)
    {
       
        Projectile bulletScript = bullet.GetComponent<Projectile>();

        if (bulletScript != null)
        {
            float originalSpeed = bulletScript.speed;

            bulletScript.speed = 0f;

            yield return new WaitForSeconds(1f);

            bulletScript.speed = originalSpeed;
        }
    }
    void SpawnFlame()
    {
        int numberOfFlames = Random.Range(10, 21);
        for (int i = 0; i < numberOfFlames; i++)
        {
            float randomX = Random.Range(-10f, 50f);
            Vector2 spawnPoint = new Vector2(randomX, transform.position.y+10);

            GameObject flame = Instantiate(flameBallBossPrefab, spawnPoint, Quaternion.identity);

            
            FlameBall_BarcoBoss_Attack flameScript = flame.GetComponent<FlameBall_BarcoBoss_Attack>();
            if (flameScript != null)
            {
                flameScript.speed = Random.Range(2f, 10f);
            }
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
        if (collision.gameObject.tag == "Player" && !invulnerable)
        {
            bossHealth -= 10;
            BossLifeBar.value = bossHealth;
            StartCoroutine(InvulnerabilityPeriod());
        }
    }
    IEnumerator InvulnerabilityPeriod()
    {
        invulnerable = true;
        BossLifeBar.fillRect.GetComponent<Image>().color = Color.cyan;
        yield return new WaitForSeconds(3f);
        invulnerable = false;
        BossLifeBar.fillRect.GetComponent<Image>().color = Color.white;
    }
}
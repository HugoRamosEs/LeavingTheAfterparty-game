using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class BarcoBossFight : MonoBehaviour
{
    private Transform player;
    private Player playerStatus;
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
    public GameObject bloqueoTop;
    public GameObject perla;
    public GameObject llave;
    public Canvas canvas;
    private bool inPhaseTransition = false;

    private GameObject playerStats;


    void Start()
    {
        animator = GetComponent<Animator>();
        MaxHealth = bossHealth;
        InvokeRepeating("FireMagicBullet", 5f, 1000f);
        InvokeRepeating("SpawnFlame", 8f, 1000f);
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
            playerStatus = player.GetComponent<Player>();
        }

        if (playerStatus.isDead)
        {
            BossLifeBar.gameObject.SetActive(false);
        }

        if (!inPhaseTransition && !playerStatus.isDead && bossHealth > 0)
        {
            BossLifeBar.gameObject.SetActive(true);
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
            // BossLifeBar.gameObject.SetActive(false);
            StartCoroutine(PhaseTransition("¿Que ha sido eso?", true));
            bloqueoTop.SetActive(false);
            perla.SetActive(true);
            llave.SetActive(true);
            // panelTransition.SetActive(false);
        }
    }

    IEnumerator PhaseTransition(string phaseText, bool isBossDead = false)
    {
        inPhaseTransition = true;
        BarcoBossTransformation.KeepPlayerStill = true;
        float transitionTime = 1f;
        float panelTransitionTime = 0.7f;
        canvas.sortingOrder = 2;
        BossLifeBar.gameObject.SetActive(false);

        Image panelImage = panelTransition.GetComponent<Image>();
        Color panelColor = panelImage.color;
        for (float t = 0; t <= 1; t += Time.fixedDeltaTime / panelTransitionTime)
        {
            float clampedT = Mathf.Clamp01(t);
            panelColor.a = Mathf.Lerp(0, 1f, clampedT);
            panelImage.color = panelColor;
            yield return null;
        }

        textTransition.text = phaseText;
        Color textColor = textTransition.color;
        for (float t = 0; t <= 1; t += Time.fixedDeltaTime / transitionTime)
        {
            float clampedT = Mathf.Clamp01(t);
            textColor.a = Mathf.Lerp(0, 1, clampedT);
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

        yield return new WaitForSecondsRealtime(2f);

        for (float t = 0; t <= 1; t += Time.unscaledDeltaTime / transitionTime)
        {
            float clampedT = Mathf.Clamp01(t);
            textColor.a = Mathf.Lerp(1, 0, clampedT);
            textTransition.color = textColor;
            yield return null;
        }

        for (float t = 0; t <= 1; t += Time.unscaledDeltaTime / panelTransitionTime)
        {
            float clampedT = Mathf.Clamp01(t);
            panelColor.a = Mathf.Lerp(1f, 0, clampedT);
            panelImage.color = panelColor;
            yield return null;
        }

        if (isBossDead)
        {
            colisionMuro.SetActive(false);
            muroNoHitbox.SetActive(false);
            Destroy(gameObject);
        }

        if (!isBossDead)
        {
            BossLifeBar.gameObject.SetActive(true);
        }

        canvas.sortingOrder = 1;
        inPhaseTransition = false;
        BarcoBossTransformation.KeepPlayerStill = false;
    }

    void FireMagicBullet()
    {
        if (inPhaseTransition) return;

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
        if (inPhaseTransition) return;

        int numberOfFlames = Random.Range(10, 21);
        for (int i = 0; i < numberOfFlames; i++)
        {
            float randomX = Random.Range(-10f, 50f);
            Vector2 spawnPoint = new Vector2(randomX, transform.position.y + 10);

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

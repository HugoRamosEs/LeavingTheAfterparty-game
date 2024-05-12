using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarcoBossFight : MonoBehaviour
{
    private bool step2 = false;
    private bool step3 = false;
    private bool inPhaseTransition = false;
    private bool finalPhaseStarted = false;
    private int maxHealth;
    private Player playerStatus;
    private Animator animator;
    private Transform player;

    public int bossHealth;
    public static bool invulnerable = false;
    public GameObject panelTransition;
    public GameObject perla;
    public GameObject llave;
    public GameObject bloqueoTop;
    public GameObject muroNoHitbox;
    public GameObject colisionMuro;
    public GameObject flameBallBossPrefab;
    public GameObject magicBulletBossPrefab;
    public GameObject anciano;
    public GameObject bossFightController;
    public Slider bossLifeBar;
    public Canvas canvas;
    public TextMeshProUGUI textTransition;
    public SceneChange sceneChange;
    public ChangeSong audioBoss;
    public EnemyHealth enemyHealth;

    void OnEnable()
    {
        EnemyHealth.OnHealthChanged += UpdateBossHealth;
    }

    void OnDisable()
    {
        EnemyHealth.OnHealthChanged -= UpdateBossHealth;
    }

    private void UpdateBossHealth(int currentHealth)
    {
        if (!invulnerable)
        {
            if (currentHealth < bossHealth)
            {
                StartCoroutine(InvulnerabilityPeriod());
            }

            bossHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            bossLifeBar.value = bossHealth;
            CheckForPhaseTransition();
        }
    }

    private void CheckForPhaseTransition()
    {
        if (bossHealth <= maxHealth * 0.66 && !step2)
        {
            ChangePhase("El aire es más frío a tu alrededor...", 13f, 2f);
            step2 = true;
        }
        else if (bossHealth <= maxHealth * 0.33 && !step3)
        {
            ChangePhase("Se avecina un infierno inminente...", 1f);
            step3 = true;
        }
        else if (bossHealth <= 1 && !finalPhaseStarted)
        {
            finalPhaseStarted = true;
            EndPhase("¿Qué ha sido eso?", true);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        maxHealth = bossHealth;
        InvokeRepeating("FireMagicBullet", 5f, 5f);
        InvokeRepeating("SpawnFlame", 8f, 8f);
        bossLifeBar.maxValue = bossHealth;
        bossLifeBar.value = bossHealth;
        colisionMuro.SetActive(true);
        muroNoHitbox.SetActive(true);
        bloqueoTop.SetActive(true);

        if (PlayerSceneController.barcoBossPasado)
        {
            colisionMuro.SetActive(false);
            muroNoHitbox.SetActive(false);
            bloqueoTop.SetActive(false);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (player == null)
        {
            CheckForPlayerWithTag();
            if (player != null)
            {
                playerStatus = player.GetComponent<Player>();
            }    
        }

        if (playerStatus != null && playerStatus.isDead)
        {
            bossLifeBar.gameObject.SetActive(false);
        }

        if (!inPhaseTransition && playerStatus != null && !playerStatus.isDead && bossHealth > 0)
        {
            bossLifeBar.gameObject.SetActive(true);
        }

        animator.SetInteger("Health", bossHealth);

        if(playerStatus.isDead)
        {
            ResetBossFight();
        }
    }

    void ChangePhase(string phaseText, float magicBulletDelay, float flameDelay = 0f)
    {
        CancelInvoke("FireMagicBullet");
        CancelInvoke("SpawnFlame");
        StartCoroutine(PhaseTransition(phaseText));
        InvokeRepeating("FireMagicBullet", magicBulletDelay, magicBulletDelay);
        if (flameDelay > 0f)
        {
            InvokeRepeating("SpawnFlame", flameDelay, flameDelay);
        } 
    }

    void EndPhase(string phaseText, bool isBossDead = false)
    {
        CancelInvoke("FireMagicBullet");
        CancelInvoke("SpawnFlame");
        StartCoroutine(PhaseTransition(phaseText, isBossDead));
    }

    void ResetBossFight()
    {
        StopFireAndFlameGeneration();
        audioBoss.StopSong();
        audioBoss.ChangeToSceneSong();
        invulnerable = false;
        bossLifeBar.fillRect.GetComponent<Image>().color = Color.white;
        bossLifeBar.gameObject.SetActive(false);
        anciano.SetActive(true);
        bossFightController.SetActive(true);
        colisionMuro.SetActive(false);
        muroNoHitbox.SetActive(false);
        bloqueoTop.SetActive(false);
        bossHealth = 101;
        enemyHealth.health = 101;
        enemyHealth.healthBar.UpdateHealthBar(101, 101);
        bossLifeBar.value = 101;
        step2 = false;
        step3 = false;
        gameObject.SetActive(false);
    }

    IEnumerator PhaseTransition(string phaseText, bool isBossDead = false)
    {
        inPhaseTransition = true;
        BarcoBossTransformation.KeepPlayerStill = true;
        float transitionTime = 1f;
        float panelTransitionTime = 0.30f;
        canvas.sortingOrder = 2;
        bossLifeBar.gameObject.SetActive(false);

        if (playerStatus != null)
        {
            playerStatus.isInvulnerable = true;
        }

        Image panelImage = panelTransition.GetComponent<Image>();
        Color panelColor = panelImage.color;
        panelColor.a = 0;
        panelImage.color = panelColor;
        yield return StartCoroutine(ChangeAlpha(panelImage, 0, 1, panelTransitionTime));

        if (isBossDead)
        {
            SpriteRenderer bossSpriteRenderer = GetComponent<SpriteRenderer>();
            if (bossSpriteRenderer != null)
            {
                bossSpriteRenderer.color = new Color(bossSpriteRenderer.color.r, bossSpriteRenderer.color.g, bossSpriteRenderer.color.b, 0);
            }
        }

        textTransition.text = phaseText;
        Color textColor = textTransition.color;
        textColor.a = 0;
        textTransition.color = textColor;
        yield return StartCoroutine(ChangeAlphaText(textTransition, 0, 1, transitionTime));

        yield return new WaitForSecondsRealtime(2f);

        yield return StartCoroutine(ChangeAlphaText(textTransition, 1, 0, transitionTime));
        yield return StartCoroutine(ChangeAlpha(panelImage, 1, 0, panelTransitionTime));

        if (isBossDead)
        {
            audioBoss.ChangeToSceneSong();
            perla.SetActive(true);
            llave.SetActive(true);
            bloqueoTop.SetActive(false);
            colisionMuro.SetActive(false);
            muroNoHitbox.SetActive(false);
            PlayerSceneController.barcoBossPasado = true;
            Destroy(gameObject);
        }

        if (!isBossDead)
        {
            bossLifeBar.gameObject.SetActive(true);
        }

        if (playerStatus != null)
        {
            playerStatus.isInvulnerable = false;
        }

        canvas.sortingOrder = 1;
        inPhaseTransition = false;
        BarcoBossTransformation.KeepPlayerStill = false;
    }

    IEnumerator ChangeAlpha(Image image, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.SmoothStep(startAlpha, endAlpha, elapsedTime / duration));
            yield return null;
            elapsedTime += Time.unscaledDeltaTime;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, endAlpha);
    }

    IEnumerator ChangeAlphaText(TextMeshProUGUI text, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.SmoothStep(startAlpha, endAlpha, elapsedTime / duration));
            yield return null;
            elapsedTime += Time.unscaledDeltaTime;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, endAlpha);
    }

    void FireMagicBullet()
    {
        if (inPhaseTransition || player == null) return;

        float radius = 10f;
        Vector2 spawnPoint = (Vector2)player.position + Random.insideUnitCircle * radius;

        Scene barcoScene = SceneManager.GetSceneByName("BarcoScene");
        SceneManager.SetActiveScene(barcoScene);

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

        Scene barcoScene = SceneManager.GetSceneByName("BarcoScene");
        SceneManager.SetActiveScene(barcoScene);

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

    void StopFireAndFlameGeneration()
    {
        CancelInvoke("FireMagicBullet");
        CancelInvoke("SpawnFlame");
        GameObject[] fireballs = GameObject.FindGameObjectsWithTag("Fireball");
        foreach (GameObject fireball in fireballs)
        {
            Destroy(fireball);
        }

        GameObject[] magicBullets = GameObject.FindGameObjectsWithTag("MagicBullet");
        foreach (GameObject magicBullet in magicBullets)
        {
            Destroy(magicBullet);
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

    IEnumerator InvulnerabilityPeriod()
    {
        invulnerable = true;
        bossLifeBar.fillRect.GetComponent<Image>().color = Color.cyan;
        yield return new WaitForSeconds(3f);
        invulnerable = false;
        bossLifeBar.fillRect.GetComponent<Image>().color = Color.white;
    }
}

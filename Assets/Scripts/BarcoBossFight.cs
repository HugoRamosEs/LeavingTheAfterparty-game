using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BarcoBossFight : MonoBehaviour
{
    [Header("Boss Properties")]
    public int bossHealth = 100;
    private int maxHealth;
    private bool step2 = false;
    private bool step3 = false;
    private bool invulnerable = false;
    private bool inPhaseTransition = false;
    private Animator animator;

    [Header("Player & UI")]
    private Transform player;
    private Player playerStatus;
    public Slider bossLifeBar;
    public GameObject panelTransition;
    public TextMeshProUGUI textTransition;
    public Canvas canvas;

    [Header("Prefabs & Game Objects")]
    public GameObject magicBulletBossPrefab;
    public GameObject flameBallBossPrefab;
    public GameObject colisionMuro;
    public GameObject muroNoHitbox;
    public GameObject bloqueoTop;
    public GameObject perla;
    public GameObject llave;

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
    }

    void Update()
    {
        if (player == null)
        {
            CheckForPlayerWithTag();
            if (player != null)
                playerStatus = player.GetComponent<Player>();
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
        else if (bossHealth <= 0)
        {
            EndPhase("¿Que ha sido eso?", true);
        }
    }

    void ChangePhase(string phaseText, float magicBulletDelay, float flameDelay = 0f)
    {
        CancelInvoke("FireMagicBullet");
        CancelInvoke("SpawnFlame");
        StartCoroutine(PhaseTransition(phaseText));
        InvokeRepeating("FireMagicBullet", magicBulletDelay, magicBulletDelay);
        if (flameDelay > 0f)
            InvokeRepeating("SpawnFlame", flameDelay, flameDelay);
    }

    void EndPhase(string phaseText, bool isBossDead = false)
    {
        CancelInvoke("FireMagicBullet");
        CancelInvoke("SpawnFlame");
        StartCoroutine(PhaseTransition(phaseText, isBossDead));
    }

    IEnumerator PhaseTransition(string phaseText, bool isBossDead = false)
    {
        inPhaseTransition = true;
        BarcoBossTransformation.KeepPlayerStill = true;
        float transitionTime = 1f; // Tiempo para cambiar la opacidad del texto
        float panelTransitionTime = 0.30f; // Tiempo para cambiar la opacidad del panel
        canvas.sortingOrder = 2;
        bossLifeBar.gameObject.SetActive(false);

        // Inicia la transición del panel
        Image panelImage = panelTransition.GetComponent<Image>();
        Color panelColor = panelImage.color;
        panelColor.a = 0; // Inicializa la opacidad del panel a 0
        panelImage.color = panelColor;
        yield return StartCoroutine(ChangeAlpha(panelImage, 0, 1, panelTransitionTime)); // Sube la opacidad del panel

        // Configura y muestra el texto de transición
        textTransition.text = phaseText;
        Color textColor = textTransition.color;
        textColor.a = 0; // Inicializa la opacidad del texto a 0
        textTransition.color = textColor;
        yield return StartCoroutine(ChangeAlphaText(textTransition, 0, 1, transitionTime)); // Sube la opacidad del texto

        yield return new WaitForSecondsRealtime(2f); // Mantiene el texto visible

        yield return StartCoroutine(ChangeAlphaText(textTransition, 1, 0, transitionTime)); // Baja la opacidad del texto
        yield return StartCoroutine(ChangeAlpha(panelImage, 1, 0, panelTransitionTime)); // Baja la opacidad del panel

        if (isBossDead)
        {
            perla.SetActive(true);
            llave.SetActive(true);
            bloqueoTop.SetActive(false);
            colisionMuro.SetActive(false);
            muroNoHitbox.SetActive(false);
            Destroy(gameObject);
        }

        if (!isBossDead)
        {
            bossLifeBar.gameObject.SetActive(true);
        }

        canvas.sortingOrder = 1;
        inPhaseTransition = false;
        BarcoBossTransformation.KeepPlayerStill = false;
    }

    // Corrutina para cambiar la opacidad de un Image
    IEnumerator ChangeAlpha(Image image, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.SmoothStep(startAlpha, endAlpha, elapsedTime / duration));
            yield return null;
            elapsedTime += Time.unscaledDeltaTime;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, endAlpha); // Asegura el valor final de la opacidad
    }

    // Corrutina para cambiar la opacidad de un TextMeshProUGUI
    IEnumerator ChangeAlphaText(TextMeshProUGUI text, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.SmoothStep(startAlpha, endAlpha, elapsedTime / duration));
            yield return null;
            elapsedTime += Time.unscaledDeltaTime;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, endAlpha); // Asegura el valor final de la opacidad
    }

    void FireMagicBullet()
    {
        if (inPhaseTransition || player == null) return;

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
        if (collision.gameObject.CompareTag("Player") && !invulnerable)
        {
            bossHealth -= 10;
            bossLifeBar.value = bossHealth;
            StartCoroutine(InvulnerabilityPeriod());
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

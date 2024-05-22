using UnityEngine;

/// <summary>
/// Script for the bullet prefab. It handles the bullet's movement and collision.
/// </summary>
public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 initialPosition;
    private Camera mainCam;
    private Rigidbody2D rb;

    public int damage = 25;
    public float force;
    public float maxDistance = 10f;
    public GameObject destructionParticles;
  
    /// <summary>
    /// Start is called before the first frame update, it gets the main camera and the rigidbody of the bullet.
    /// </summary>
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        initialPosition = transform.position;
    }

    /// <summary>
    /// Update is called once per frame, it checks if the bullet has reached the max distance and destroys it if it has.
    /// </summary>
    void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) > maxDistance)
        {
            DestroyProjectile();
        }
    }

    /// <summary>
    /// It checks if the bullet has collided with an enemy and deals damage to it.
    /// </summary>
    /// <param name="collision"> the enemy collision </param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            DestroyProjectile();
        }
    }

    /// <summary>
    /// It destroys the bullet and instantiates the destruction particles.
    /// </summary>
    void DestroyProjectile()
    {
        Instantiate(destructionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

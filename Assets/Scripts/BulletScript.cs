using UnityEngine;

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

    void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) > maxDistance)
        {
            DestroyProjectile();
        }
    }

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

    void DestroyProjectile()
    {
        Instantiate(destructionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

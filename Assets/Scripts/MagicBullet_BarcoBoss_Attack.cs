using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform player;
    public float damage = 10f;
    [SerializeField]
    private float bulletSpeed = 6f;
    [SerializeField]
    private float rotateSpeed = 1000f;
    [SerializeField]
    private float lifeTime = 5f; // Duración de la bola de energía en segundos

    private float timer = 0f; // Contador de tiempo

    void Start()
    {
        if (player == null) {
            CheckForPlayerWithTag();
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        }

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
            }
            DestroyProjectile();
        
        }
    }
    void DestroyProjectile() {
        Destroy(gameObject);
    }

    void CheckForPlayerWithTag()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
}
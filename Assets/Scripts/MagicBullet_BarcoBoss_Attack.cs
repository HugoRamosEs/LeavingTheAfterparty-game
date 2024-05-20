using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script is used to control the magic bullet of the boss of the ship level.
/// </summary>
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

    /// <summary>
    /// This method is called is used to check for the player object in the scene at the start of the script execution.
    /// </summary>
    void Start()
    {
        if (player == null) {
            CheckForPlayerWithTag();
        }
    }

    /// <summary>
    /// This method is called once per frame and is used to move the magic bullet towards the player.
    /// </summary>
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

/// <summary>
/// This method is called when the magic bullet collides with the player object.
/// </summary>
/// <param name="collision"> Player's collision</param>
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
    /// <summary>
    /// This method destroys the magic bullet object.
    /// </summary>
    void DestroyProjectile() {
        Destroy(gameObject);
    }

    /// <summary>
    /// This method is used to check for the player object in the scene.
    /// </summary>
    void CheckForPlayerWithTag()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
}
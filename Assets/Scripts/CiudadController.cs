using UnityEngine;

/// <summary>
/// Checks if the enemies in the city have been defeated and updates the PlayerSceneController accordingly.
/// </summary>
public class CiudadController : MonoBehaviour
{
    private EnemyHealth healthArqueroBoss;
    private EnemyHealth healthArquero1;
    private EnemyHealth healthArquero2;

    public GameObject arqueroBoss;
    public GameObject arquero1;
    public GameObject arquero2;

    /// <summary>
    /// Initializes the health of the enemies, and deactivates them if all the enemies have been defeated.
    /// </summary>
    void Start()
    {
        healthArqueroBoss = arqueroBoss.GetComponent<EnemyHealth>();
        healthArquero1 = arquero1.GetComponent<EnemyHealth>();
        healthArquero2 = arquero2.GetComponent<EnemyHealth>();

        if (PlayerSceneController.ciudadBossPasado)
        {
            arqueroBoss.SetActive(false);
            arquero1.SetActive(false);
            arquero2.SetActive(false);
        }
    }

    /// <summary>
    /// Updates the PlayerSceneController if all enemies have been defeated.
    /// </summary>
    void Update()
    {
        if (healthArqueroBoss.health <= 0 && healthArquero1.health <= 0 && healthArquero2.health <= 0)
        {
            PlayerSceneController.ciudadBossPasado = true;
        }
    }
}

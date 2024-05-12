using UnityEngine;

public class CiudadController : MonoBehaviour
{
    private EnemyHealth healthArqueroBoss;
    private EnemyHealth healthArquero1;
    private EnemyHealth healthArquero2;

    public GameObject arqueroBoss;
    public GameObject arquero1;
    public GameObject arquero2;

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

    void Update()
    {
        if (healthArqueroBoss.health <= 0 && healthArquero1.health <= 0 && healthArquero2.health <= 0)
        {
            PlayerSceneController.ciudadBossPasado = true;
        }
    }
}

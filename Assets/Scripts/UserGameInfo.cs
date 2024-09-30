using UnityEngine;

public class UserGameInfo : MonoBehaviour
{
    public static UserGameInfo Instance { get; private set; }

    public string email;
    public string username;

    public string idPartida;
    public string escenaPartida;
    public string posX;
    public string posY;
    public string posZ;
    public string currentHp;
    public string currentStamina;
    public string inventario;
    public string orderInLayer;
    public string sotanoPasado;
    public string congeladorPasado;
    public string playaPasada;
    public string barcoBossPasado;
    public string ciudadBossPasado;
    public string luzSotanoEncendida;
    public string donutDesbloqueado;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        email = PlayerPrefs.GetString("Correo");
        username = PlayerPrefs.GetString("NombreUsuario");
    }

    /// <summary>
    /// Updates the user information from player prefs.
    /// </summary>
    public void UpdateUserInfo()
    {
        email = PlayerPrefs.GetString("Correo", "");
        username = PlayerPrefs.GetString("NombreUsuario", "");
    }

    /// <summary>
    /// Updates the game information with provided data.
    /// </summary>
    public void UpdateGameInfo(string id, string escena, string x, string y, string z, string hp, string stamina,
        string layer, string sotano, string congelador, string playa, string barcoBoss, string ciudadBoss,
        string luzOn, string donut)
    {
        idPartida = id;
        escenaPartida = escena;
        posX = x;
        posY = y;
        posZ = z;
        currentHp = hp;
        currentStamina = stamina;
        orderInLayer = layer;
        sotanoPasado = sotano;
        congeladorPasado = congelador;
        playaPasada = playa;
        barcoBossPasado = barcoBoss;
        ciudadBossPasado = ciudadBoss;
        luzSotanoEncendida = luzOn;
        donutDesbloqueado = donut;
    }

    /// <summary>
    /// Loads player data into the provided player object.
    /// </summary>
    public void LoadPlayerData(Player player)
    {
        if (!string.IsNullOrEmpty(idPartida) &&
            !string.IsNullOrEmpty(escenaPartida) &&
            !string.IsNullOrEmpty(posX) &&
            !string.IsNullOrEmpty(posY) &&
            !string.IsNullOrEmpty(posZ) &&
            !string.IsNullOrEmpty(currentHp) &&
            !string.IsNullOrEmpty(currentStamina) &&
            !string.IsNullOrEmpty(orderInLayer) &&
            !string.IsNullOrEmpty(sotanoPasado) &&
            !string.IsNullOrEmpty(congeladorPasado) &&
            !string.IsNullOrEmpty(playaPasada) &&
            !string.IsNullOrEmpty(barcoBossPasado) &&
            !string.IsNullOrEmpty(ciudadBossPasado) &&
            !string.IsNullOrEmpty(luzSotanoEncendida) &&
            !string.IsNullOrEmpty(donutDesbloqueado)
            )
        {
            float.TryParse(posX, out float x);
            float.TryParse(posY, out float y);
            float.TryParse(posZ, out float z);
            float.TryParse(currentHp, out float hp);
            float.TryParse(currentStamina, out float stamina);

            player.transform.position = new Vector3(x, y, z);
            player.hp.currentVal = hp;
            player.stamina.currentVal = stamina;

            if (int.TryParse(orderInLayer, out int layer))
            {
                player.GetComponent<SpriteRenderer>().sortingOrder = layer;
            }

            PlayerSceneController.sotanoPasado = bool.Parse(sotanoPasado);
            PlayerSceneController.congeladorPasado = bool.Parse(congeladorPasado);
            PlayerSceneController.playaPasada = bool.Parse(playaPasada);
            PlayerSceneController.barcoBossPasado = bool.Parse(barcoBossPasado);
            PlayerSceneController.ciudadBossPasado = bool.Parse(ciudadBossPasado);
            PlayerSceneController.luzSotanoEncendida = bool.Parse(luzSotanoEncendida);
            PlayerSceneController.donutDesbloqueado = bool.Parse(donutDesbloqueado);
        }
        else
        {
            Debug.LogError("No se puede cargar los datos del jugador porque falta información.");
        }
    }
}

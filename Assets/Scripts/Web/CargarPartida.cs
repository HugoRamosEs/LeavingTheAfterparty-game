using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarPartida : MonoBehaviour
{
    public Server servidor;
    public GameObject imLoading;
    public GameObject imRespuestaScene;
    public TextMeshProUGUI mensajeText;

    private bool cargando = false;

    /// <summary>
    /// Starts the process to load the game.
    /// </summary>
    public void Cargar()
    {
        StartCoroutine(CargarDatos());
    }

    /// <summary>
    /// Coroutine to load game data.
    /// </summary>
    /// <returns>An IEnumerator to handle the coroutine.</returns>
    IEnumerator CargarDatos()
    {
        cargando = true;
        //imLoading.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        string[] datos = new string[1];
        datos[0] = UserGameInfo.Instance.email;
        StartCoroutine(servidor.ConsumirServicio("cargar-partida", datos, PosCargar));
        //imLoading.SetActive(false);
        yield return new WaitUntil(() => !servidor.ocupado);
        yield return new WaitWhile(() => cargando);
    }

    /// <summary>
    /// Handles the server response after loading data.
    /// </summary>
    void PosCargar()
    {
        mensajeText.text = servidor.respuesta.mensaje;

        switch (servidor.respuesta.codigo)
        {
            case 207: // El correo no esta registrado
                Debug.Log("El correo no esta registrado");
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 212: // Cargado correctamente
                Debug.Log(servidor.respuesta.mensaje);
                ProcesarRespuesta(servidor.respuesta.respuesta);
                break;
            case 214: // No se ha encontrado una partida para cargar. Por favor, cree una nueva
                Debug.Log(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 401: // Error intentando conectar
                Debug.Log(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 404:
                Debug.Log(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 0: // Error
                Debug.Log("Error, no se puede conectar con el servidor.");
                StartCoroutine(MostrarYEsconderEscena());
                break;
            default:
                break;
        }
        cargando = false;
    }

    /// <summary>
    /// Coroutine to show and hide the response scene.
    /// </summary>
    /// <returns>An IEnumerator to handle the coroutine.</returns>
    IEnumerator MostrarYEsconderEscena()
    {
        cargando = false;
        imRespuestaScene.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        imRespuestaScene.SetActive(false);
    }

    /// <summary>
    /// Processes the server response.
    /// </summary>
    /// <param name="respuesta">The response string from the server.</param>
    void ProcesarRespuesta(string respuesta)
    {
        cargando = false;
        Debug.Log("Respuesta en ProcesarRespuesa: " + respuesta);

        string[] pares = respuesta.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);

        string id = "";
        string escena = "";
        float posX = 0f;
        float posY = 0f;
        float posZ = 0f;
        float currentHp = 100f;
        float currentStamina = 200f;
        int orderInLayer = 6;
        bool sotanoPasado = false;
        bool congeladorPasado = false;
        bool playaPasada = false;
        bool barcoBossPasado = false;
        bool ciudadBossPasado = false;
        bool luzSotanoEncendida = false;
        bool donutDesbloqueado = false;

        foreach (string par in pares)
        {
            string[] partes = par.Split(':');
            if (partes.Length == 2)
            {
                string clave = partes[0].Trim();
                string valor = partes[1].Trim();

                switch (clave)
                {
                    case "id":
                        id = valor;
                        break;
                    case "escena":
                        escena = valor;
                        break;
                    case "posX":
                        posX = stringToFloat(valor);
                        break;
                    case "posY":
                        posY = stringToFloat(valor);
                        break;
                    case "posZ":
                        posZ = stringToFloat(valor);
                        break;
                    case "currentHp":
                        currentHp = stringToFloat(valor);
                        break;
                    case "currentStamina":
                        currentStamina = stringToFloat(valor);
                        break;
                    case "orderInLayer":
                        if (int.TryParse(valor, out int layerResult))
                        {
                            orderInLayer = layerResult;
                        }
                        break;
                    case "sotanoPasado":
                        sotanoPasado = ConvertToBool(valor);
                        break;
                    case "congeladorPasado":
                        congeladorPasado = ConvertToBool(valor);
                        break;
                    case "playaPasada":
                        playaPasada = ConvertToBool(valor);
                        break;
                    case "barcoBossPasado":
                        barcoBossPasado = ConvertToBool(valor);
                        break;
                    case "ciudadBossPasado":
                        ciudadBossPasado = ConvertToBool(valor);
                        break;
                    case "luzSotanoEncendida":
                        luzSotanoEncendida = ConvertToBool(valor);
                        break;
                    case "donutDesbloqueado":
                        donutDesbloqueado = ConvertToBool(valor);
                        break;
                }
            }
        }

        Debug.Log("Vida: " + currentHp.ToString());
        Debug.Log("Estamina: " + currentStamina.ToString());

        UserGameInfo.Instance.UpdateGameInfo(id, escena, posX.ToString(), posY.ToString(), posZ.ToString(), currentHp.ToString("F2"), currentStamina.ToString("F2"),
            orderInLayer.ToString(), sotanoPasado.ToString(), congeladorPasado.ToString(), playaPasada.ToString(), barcoBossPasado.ToString(), ciudadBossPasado.ToString(),
            luzSotanoEncendida.ToString(), donutDesbloqueado.ToString());

        CargarEscenas(escena);
    }

    /// <summary>
    /// Converts a string to a boolean value.
    /// </summary>
    /// <param name="value">The string value to convert.</param>
    /// <returns>A boolean representation of the string.</returns>
    bool ConvertToBool(string value)
    {
        if (bool.TryParse(value, out bool result))
        {
            return result;
        }
        else
        {
            if (int.TryParse(value, out int intResult))
            {
                return intResult != 0;
            }
        }

        Debug.LogError($"No se pudo convertir '{value}' a un valor booleano.");
        return false;
    }

    /// <summary>
    /// Converts a string to a float value.
    /// </summary>
    /// <param name="posicionDecimal">The string value to convert.</param>
    /// <returns>A float representation of the string.</returns>
    float stringToFloat(string posicionDecimal)
    {
        posicionDecimal = posicionDecimal.Replace('.', ',');

        if (float.TryParse(posicionDecimal, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("es-ES"), out float valorFlotante))
        {
            return valorFlotante;
        }
        else
        {
            Debug.LogError("No se pudo convertir la posici�n a un n�mero flotante.");
            return 0f;
        }
    }

    /// <summary>
    /// Loads the specified scenes.
    /// </summary>
    /// <param name="escenaNombre">The name of the main scene to load.</param>
    void CargarEscenas(string escenaNombre)
    {
        SceneManager.LoadScene(escenaNombre);
        SceneManager.LoadScene("EsencialScene", LoadSceneMode.Additive);
    }


}

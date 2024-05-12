using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CargarPartida : MonoBehaviour
{
    public Server servidor;
    public GameObject imLoading;
    public GameObject imRespuestaScene;
    public TextMeshProUGUI mensajeText;

    private bool cargando = false;

    public void Cargar()
    {
        StartCoroutine(CargarDatos());
    }

    IEnumerator CargarDatos()
    {
        cargando = true;
        imLoading.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Entro en CargandoDatos()");

        string[] datos = new string[1];
        datos[0] = UserGameInfo.Instance.email;

        StartCoroutine(servidor.ConsumirServicio("cargar-partida", datos, PosCargar));
        yield return new WaitUntil(() => !servidor.ocupado);
        yield return new WaitWhile(() => cargando);
        imLoading.SetActive(false);
        Debug.Log("Acabe CargandoDatos()");
    }

    void PosCargar()
    {

        Debug.Log(mensajeText.text);
        mensajeText.text = servidor.respuesta.mensaje;

        switch (servidor.respuesta.codigo)
        {
            case 207: // El correo no esta registrado
                Debug.Log("El correo no esta registrado");
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 212: // Cargado correctamente
                Debug.Log(servidor.respuesta.mensaje);
                Debug.Log("entro en 212");
                cargarPartidaAlJuego(servidor.respuesta.respuesta.ToString());
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

    IEnumerator MostrarYEsconderEscena()
    {
        cargando = false;
        imRespuestaScene.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        imRespuestaScene.SetActive(false);
    }

    public void cargarPartidaAlJuego (string respuesta)
    {
        Debug.Log("Respuesta en el metodo: " + respuesta);
    }
}

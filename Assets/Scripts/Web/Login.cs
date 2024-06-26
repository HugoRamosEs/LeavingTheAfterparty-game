using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Login : MonoBehaviour
{
    public Server servidor;
    public TMP_InputField inpUsuario;
    public TMP_InputField inpPassword;
    public GameObject imLoading;
    public TextMeshProUGUI mensajeText;
    public GameObject imRespuestaScene;

    private bool cargando = false;

    /// <summary>
    /// Initiates the login process.
    /// </summary>
    public void IniciarSesion()
    {
        StartCoroutine(Iniciar());
    }

    /// <summary>
    /// Coroutine to initiate the login process.
    /// </summary>
    /// <returns>An IEnumerator to handle the coroutine.</returns>
    IEnumerator Iniciar()
    {
        cargando = true;

        imLoading.SetActive(true);
        string[] datos = new string[2];
        datos[0] = inpUsuario.text;
        datos[1] = inpPassword.text;

        StartCoroutine(servidor.ConsumirServicio("login", datos, PosCargar));
        //yield return new WaitForSeconds(0.5f);
        //yield return new WaitUntil(() => !servidor.ocupado);
        yield return new WaitUntil(() => !servidor.ocupado);
        yield return new WaitWhile(() => cargando);
        imLoading.SetActive(false);
    }

    /// <summary>
    /// Handles the server response after attempting to log in.
    /// </summary>
    void PosCargar()
    {
        mensajeText.text = servidor.respuesta.mensaje;
        switch (servidor.respuesta.codigo)
        {
            case 209: // Se ha iniciado de sesi�n correctamente
                print(servidor.respuesta.mensaje);
                GuardarDatosUsuario();
                SceneManager.LoadScene("MenuPrincipalScene");
                break;
            case 208: // El correo o la contrase�a son incorrectos
                print("El correo o la contrase�a son incorrectos");
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 207: // El correo no esta registrado
                print("El correo no esta registrado");
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 203: // La contrase�a no puede estar vac�a
                print("La contrase�a no puede estar vac�a");
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 201: // Direcci�n de correo no v�lida
                print("Direcci�n de correo no v�lida");
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 204: // Hi ha errors en el formulari de registre.
                print(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 401: // Error intentando conectar
                print(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 400:
                print(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 404:
                print(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 0: // Error
                print("Error, no se puede conectar con el servidor.");
                StartCoroutine(MostrarYEsconderEscena());
                break;
            default:
                break;
        }
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
    /// Saves user data locally after successful login.
    /// </summary>
    void GuardarDatosUsuario()
    {
        UserGameInfo.Instance.email = inpUsuario.text;
        UserGameInfo.Instance.username = inpUsuario.text;

        PlayerPrefs.SetString("Correo", UserGameInfo.Instance.email);
        PlayerPrefs.SetString("NombreUsuario", UserGameInfo.Instance.username);
        PlayerPrefs.Save();
    }

}

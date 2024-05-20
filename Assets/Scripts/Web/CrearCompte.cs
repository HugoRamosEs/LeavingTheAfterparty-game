using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CrearCompte : MonoBehaviour
{
    public Server servidor;
    public TMP_InputField inpCorreo;
    public TMP_InputField inpUsuario;
    public TMP_InputField inpPassword;
    public GameObject imLoading;
    public TextMeshProUGUI mensajeText;
    public GameObject imRespuestaScene;
    public GameObject imLoginScene;
    public GameObject imSignUpScene;
    public GameObject imRespuestaUsuarioCorrecto;
    public TextMeshProUGUI mensajeUsuarioCorrectoText;

    private bool cargando = false;
    public void CrearCuenta()
    {
        StartCoroutine(Iniciar());
    }
    IEnumerator Iniciar()
    {
        cargando = true;

        imLoading.SetActive(true);
        string[] datos = new string[3];
        datos[0] = inpCorreo.text;
        datos[1] = inpUsuario.text;
        datos[2] = inpPassword.text;

        StartCoroutine(servidor.ConsumirServicio("register", datos, PosCargar));
        //yield return new WaitForSeconds(0.5f);
        //yield return new WaitUntil(() => !servidor.ocupado);
        yield return new WaitUntil(() => !servidor.ocupado);
        yield return new WaitWhile(() => cargando);
        imLoading.SetActive(false);
    }
    void PosCargar()
    {
        mensajeText.text = servidor.respuesta.mensaje;
        mensajeUsuarioCorrectoText.text = servidor.respuesta.mensaje;
        switch (servidor.respuesta.codigo)
        {
            case 201: // Direcció de correu no vàlida
                print(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 202: // L'usuari no pot estar buit
                print(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 203: // La contrasenya no pot estar buida
                print(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 204: // Hi ha errors en el formulari de registre.
                print(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 205: // El correu jaa existeix.
                print(servidor.respuesta.mensaje);
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 206: // Feliciats! S+ha registrat a Leaving the After Party. En breus, se l+hi redirigirà al joc.
                print(servidor.respuesta.mensaje);
                GuardarDatosUsuario();
                StartCoroutine(MandarAlLogin());
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
            case 403: // No s'ha pogut crear l+usuari. Siusplau, avisi a l+administrador.
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
    IEnumerator MostrarYEsconderEscena()
    {
        cargando = false;
        imRespuestaScene.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        imRespuestaScene.SetActive(false);

    }

    IEnumerator MandarAlLogin()
    {
        cargando = false;
        imRespuestaUsuarioCorrecto.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        imRespuestaUsuarioCorrecto.SetActive(false);
        imSignUpScene.SetActive(false);
        SceneManager.LoadScene("MenuPrincipalScene");
    }

    void GuardarDatosUsuario()
    {
        UserGameInfo.Instance.email = inpCorreo.text;
        UserGameInfo.Instance.username = inpUsuario.text;

        PlayerPrefs.SetString("Correo", UserGameInfo.Instance.email);
        PlayerPrefs.SetString("NombreUsuario", UserGameInfo.Instance.username);
        PlayerPrefs.Save();
    }

}

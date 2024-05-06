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

    public void IniciarSesion()
    {
        StartCoroutine(Iniciar());
    }

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

    void PosCargar()
    {
        mensajeText.text = servidor.respuesta.mensaje;
        switch (servidor.respuesta.codigo)
        {
            case 209: // Se ha iniciado de sesión correctamente
                print(servidor.respuesta.mensaje);
                SceneManager.LoadScene("MenuPrincipalScene");
                break;
            case 208: // El correo o la contraseña son incorrectos
                print("El correo o la contraseña son incorrectos");
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 207: // El correo no esta registrado
                print("El correo no esta registrado");
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 203: // La contraseña no puede estar vacía
                print("La contraseña no puede estar vacía");
                StartCoroutine(MostrarYEsconderEscena());
                break;
            case 201: // Dirección de correo no válida
                print("Dirección de correo no válida");
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

    IEnumerator MostrarYEsconderEscena()
    {
        cargando = false;
        imRespuestaScene.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        imRespuestaScene.SetActive(false);
    }

}

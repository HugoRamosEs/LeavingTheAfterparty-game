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

    public void IniciarSesion()
    {
        StartCoroutine(Iniciar());
    }

    IEnumerator Iniciar()
    {
        imLoading.SetActive(true);
        string[] datos = new string[2];
        datos[0] = inpUsuario.text;
        datos[1] = inpPassword.text;

        StartCoroutine(servidor.ConsumirServicio("login", datos, PosCargar));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        imLoading.SetActive(false);
    }

    void PosCargar()
    {
        switch (servidor.respuesta.codigo)
        {
            case 209: // Se ha iniciado de sesión correctamente
                SceneManager.LoadScene("MenuPrincipal");
                break;
            case 208: // El correo o la contraseña son incorrectos
                print("El correo o la contraseña son incorrectos");
                break;
            case 207: // El correo no esta registrado
                print("El correo no esta registrado");
                break;
            case 203: // La contraseña no puede estar vacía
                print("La contraseña no puede estar vacía");
                break;
            case 201: // Dirección de correo no válida
                print("Dirección de correo no válida");
                break;
            case 401: // Error intentando conectar
                print(servidor.respuesta.mensaje);
                break;
            case 0: // Error
                print("Error, no se puede conectar con el servidor.");
                break;
            default:
                break;
        }
    }

}

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
            case 209: // Se ha iniciado de sesi�n correctamente
                SceneManager.LoadScene("MenuPrincipal");
                break;
            case 208: // El correo o la contrase�a son incorrectos
                print("El correo o la contrase�a son incorrectos");
                break;
            case 207: // El correo no esta registrado
                print("El correo no esta registrado");
                break;
            case 203: // La contrase�a no puede estar vac�a
                print("La contrase�a no puede estar vac�a");
                break;
            case 201: // Direcci�n de correo no v�lida
                print("Direcci�n de correo no v�lida");
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

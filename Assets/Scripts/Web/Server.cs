using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

/// <summary>
/// This class is used to consume services from a server.
/// </summary>
[CreateAssetMenu(fileName = "Server", menuName = "LTAP/Server", order = 1)]
public class Server : ScriptableObject
{
    public string servidor;
    public bool ocupado = false;
    public Servicio[] servicios;
    public Respuesta respuesta;

    /// <summary>
    /// Consumes a service from the server asynchronously.
    /// </summary>
    /// <param name="nombre">The name of the service to consume.</param>
    /// <param name="datos">Array of data to send to the service.</param>
    /// <param name="e">Action to execute after consuming the service.</param>
    /// <returns>An IEnumerator to handle the coroutine.</returns>
    public IEnumerator ConsumirServicio(string nombre, string[] datos, UnityAction e)
    {
        ocupado = true;
        WWWForm formulario = new WWWForm();
        Servicio s = new Servicio();

        for (int i = 0; i < servicios.Length; i++)
        {
            if (servicios[i].nombre.Equals(nombre))
            {
                s = servicios[i]; ;
            }
        }
        for (int i = 0; i < s.parametros.Length; i++)
        {
            formulario.AddField(s.parametros[i], datos[i]);
        }
        UnityWebRequest www = UnityWebRequest.Post(servidor + "/" + s.url, formulario);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            respuesta = new Respuesta();
        }
        else
        {
            string responseText = www.downloadHandler.text;
            responseText = responseText.Replace("<br />", "");
            responseText = responseText.Replace("#", "\"");
            responseText = responseText.Replace("+", "'");
            Debug.Log("Respuesta en server.cs: " + responseText);
            respuesta = JsonUtility.FromJson<Respuesta>(responseText);
        }

        ocupado = false;
        e.Invoke();
    }
}

/// <summary>
/// This class is used to store the services that the server can consume.
/// </summary>
[System.Serializable]
public class Servicio
{
    public string nombre;
    public string url;
    public string[] parametros;
}

/// <summary>
/// This class is used to store the response from the server.
/// </summary>
[System.Serializable]
public class Respuesta
{
    public int codigo;
    public string mensaje;
    public string respuesta;

    public Respuesta()
    {
        codigo = 404;
        mensaje = "Error al conectarse con el servidor.";
    }
}

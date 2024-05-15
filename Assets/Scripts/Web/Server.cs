using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Server", menuName = "LTAP/Server", order = 1)]
public class Server : ScriptableObject
{
    public string servidor;
    public Servicio[] servicios;
    public bool ocupado = false;
    public Respuesta respuesta;

    public IEnumerator ConsumirServicio(string nombre, string[] datos, UnityAction e)
    {
        ocupado = true;
        WWWForm formulario = new WWWForm();
        Servicio s = new Servicio();

        for (int i = 0; i < servicios.Length; i++)
        {
            if (servicios[i].nombre.Equals(nombre))
            {
                s = servicios[i];;
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
[System.Serializable]
public class Servicio
{
    public string nombre;
    public string url;
    public string[] parametros;
}

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardarPartida : MonoBehaviour
{
    public Server servidor;
    public GameObject imLoading;
    public Player player;
    public UltimoGuardado ultimoGuardado;
    public ItemPanel itemPanel;

    private bool cargando = false;

    public void Guardar()
    {
        StartCoroutine(GuardarDatos());
    }

    IEnumerator GuardarDatos()
    {
        cargando = true;
        imLoading.SetActive(true);
        yield return new WaitForSeconds(0.5f);


        Vector3 playerPosition = ultimoGuardado.PlayerPosition;
        string inventoryItems = GetItemPanelContents();

        string[] datos = new string[8];
        datos[0] = UserGameInfo.Instance.email;
        datos[1] = ultimoGuardado.CurrentScene;
        datos[2] = playerPosition.x.ToString();
        datos[3] = playerPosition.y.ToString();
        datos[4] = playerPosition.z.ToString();
        datos[5] = player.hp.currentVal.ToString();
        datos[6] = player.stamina.currentVal.ToString();
        datos[7] = inventoryItems;

        //Debug.Log(datos[0] + " - " + datos[1] + " - " + datos[2] + " - " + datos[3] + " - " + datos[4] + " - " + datos[5] + " - " + datos[6]);
        Debug.Log(datos[7]);

        StartCoroutine(servidor.ConsumirServicio("guardar-partida", datos, PosGuardar));
        yield return new WaitUntil(() => !servidor.ocupado);
        yield return new WaitWhile(() => cargando);
        imLoading.SetActive(false);
    }

    void PosGuardar()
    {
        switch (servidor.respuesta.codigo)
        {
            case 210: // Datos guardados correctamente
                Debug.Log(servidor.respuesta.mensaje);
                break;
            case 207: // El correo no esta registrado
                Debug.Log("El correo no esta registrado");
                break;
            case 401: // Error intentando conectar
                Debug.Log(servidor.respuesta.mensaje);
                break;
            case 404:
                Debug.Log(servidor.respuesta.mensaje);
                break;
            case 0: // Error
                Debug.Log("Error, no se puede conectar con el servidor.");
                break;
            default:
                break;
        }
        cargando = false;
    }

    public string GetItemPanelContents()
    {
        List<string> itemPanelContents = new List<string>();

        for (int i = 0; i < itemPanel.inventory.slots.Count; i++)
        {
            ItemSlot slot = itemPanel.inventory.slots[i];
            if (slot.item != null)
            {
                itemPanelContents.Add($"({i},{slot.item.Name})");
            }
        }

        return string.Join(", ", itemPanelContents);
    }
}

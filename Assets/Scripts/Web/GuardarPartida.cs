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
        SpriteRenderer playerSpriteRenderer = player.GetComponent<SpriteRenderer>();

        string[] datos = new string[16];
        datos[0] = UserGameInfo.Instance.email;
        datos[1] = ultimoGuardado.CurrentScene;
        datos[2] = playerPosition.x.ToString("F2");
        datos[3] = playerPosition.y.ToString("F2");
        datos[4] = playerPosition.z.ToString();
        datos[5] = player.hp.currentVal.ToString();
        datos[6] = player.stamina.currentVal.ToString();
        datos[7] = inventoryItems;
        datos[8] = playerSpriteRenderer != null ? playerSpriteRenderer.sortingOrder.ToString() : "0";
        datos[9] = PlayerSceneController.sotanoPasado ? "1" : "0";
        datos[10] = PlayerSceneController.congeladorPasado ? "1" : "0";
        datos[11] = PlayerSceneController.playaPasada ? "1" : "0";
        datos[12] = PlayerSceneController.barcoBossPasado ? "1" : "0";
        datos[13] = PlayerSceneController.ciudadBossPasado ? "1" : "0";
        datos[14] = PlayerSceneController.luzSotanoEncendida ? "1" : "0";
        datos[15] = PlayerSceneController.donutDesbloqueado ? "1" : "0";

        Debug.Log(datos[0] + " - " + datos[1] + " - " + datos[2] + " - " + datos[3] + " - " + datos[4] + " - " + datos[5] + " - " + datos[6] + " - " +
            datos[8] + " - " + datos[9] + " - " + datos[10] + " - " + datos[11] + " - " + datos[12] + " - " + datos[13] + " - " + datos[14] + " - " + datos[15]);
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataLoader : MonoBehaviour
{
    void Start()
    {
        // Verifica si los datos de UserGameInfo están listos para ser aplicados
        if (UserGameInfo.Instance != null && DatosValidos())
        {
            // Encuentra el objeto del jugador en la escena
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                // Aplica los datos al jugador
                UserGameInfo.Instance.LoadPlayerData(player);
            }
            else
            {
                Debug.LogError("No se encontró el objeto Player en la escena.");
            }
        }
    }

    bool DatosValidos()
    {
        // Verifica que ninguno de los datos sea nulo o vacío
        return !string.IsNullOrEmpty(UserGameInfo.Instance.idPartida) &&
               !string.IsNullOrEmpty(UserGameInfo.Instance.escenaPartida) &&
               !string.IsNullOrEmpty(UserGameInfo.Instance.posX) &&
               !string.IsNullOrEmpty(UserGameInfo.Instance.posY) &&
               !string.IsNullOrEmpty(UserGameInfo.Instance.posZ) &&
               !string.IsNullOrEmpty(UserGameInfo.Instance.currentHp) &&
               !string.IsNullOrEmpty(UserGameInfo.Instance.currentStamina);
    }
}

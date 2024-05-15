using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonCargarPartida : MonoBehaviour
{

    public Button botonCargar;

    void Start()
    {
        CargarPartida controladorCargarPartida = FindObjectOfType<CargarPartida>();
        if (controladorCargarPartida != null && botonCargar != null)
        {
            botonCargar.onClick.AddListener(controladorCargarPartida.Cargar);
        }
    }

    void OnDestroy()
    {
        if (botonCargar != null)
        {
            botonCargar.onClick.RemoveAllListeners();
        }
    }

}

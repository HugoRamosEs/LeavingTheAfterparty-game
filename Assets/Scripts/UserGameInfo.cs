using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGameInfo : MonoBehaviour
{
   public static UserGameInfo Instance { get; private set; }

    public string email;
    public string username;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        email = PlayerPrefs.GetString("Correo");
        username = PlayerPrefs.GetString("NombreUsuario");
    }

    public void UpdateUserInfo()
    {
        email = PlayerPrefs.GetString("Correo", "");
        username = PlayerPrefs.GetString("NombreUsuario", "");
    }

}

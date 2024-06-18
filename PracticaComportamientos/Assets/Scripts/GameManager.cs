using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool hayDuelo;
    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            // Si no hay una instancia, buscamos una en la escena
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                // Si no existe, creamos una nueva
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    instance = singletonObject.AddComponent<GameManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    protected GameManager() { }
    private void Awake()
    {
        hayDuelo = false;
        // Aseguramos que solo haya una instancia
        if (instance == null)
        {
            instance = this as GameManager;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}

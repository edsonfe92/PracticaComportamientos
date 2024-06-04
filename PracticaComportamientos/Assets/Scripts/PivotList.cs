using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotList : MonoBehaviour
{
    public List<Transform> Pivots;

    // La instancia estática del Singleton
    private static PivotList _instance;

    // Propiedad pública para acceder a la instancia
    public static PivotList Instance
    {
        get
        {
            // Si no hay una instancia, buscamos una en la escena
            if (_instance == null)
            {
                _instance = FindObjectOfType<PivotList>();

                // Si no existe, creamos una nueva
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<PivotList>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }
    protected PivotList() { }

    private void Awake()
    {
        // Aseguramos que solo haya una instancia
        if (_instance == null)
        {
            _instance = this as PivotList;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

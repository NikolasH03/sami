using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorVFX : MonoBehaviour
{
    [SerializeField] GameObject ataquePrefab;

    public static ControladorVFX instance;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


    }

public void GenerarEfecto(Vector3 posicion)
    {
        GameObject explosion = Instantiate(ataquePrefab, posicion, Quaternion.identity);
        Destroy(explosion, 0.8f);
    }

}

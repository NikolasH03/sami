using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Nueva Arma", menuName = "Arma")]
public class ArmaData : ScriptableObject
{

    public string nombre;

    public int dañoGolpeFuerte;
    public int dañoGolpeLigero;
    public int dañoGolpeFuerteGuardia;
    public int dañoGolpeLigeroGuardia;

    public GameObject prefab;


}

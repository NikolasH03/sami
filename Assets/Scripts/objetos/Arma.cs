using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Nueva Arma", menuName = "Arma")]
public class Arma : ScriptableObject
{

    public string nombre;

    public int dañoGolpeFuerte;
    public int dañoGolpeLigero;

    public GameObject prefab;
}

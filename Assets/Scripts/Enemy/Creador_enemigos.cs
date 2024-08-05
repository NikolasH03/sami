using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creador_enemigos : MonoBehaviour
{
    public GameObject Enemigos;
    public float tiempoCreacion = 2f, RangoCreacion = 2f;
    void Start()
    {
        InvokeRepeating ("Creando",0.0f, tiempoCreacion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Creando()
    {
        Vector3 SpawnPosition = new Vector3(0, 0, 0);
        SpawnPosition = this.transform.position + Random.onUnitSphere * RangoCreacion;
        SpawnPosition = new Vector3(SpawnPosition.x, 0, 0);

        GameObject Enemigo = Instantiate (Enemigos, SpawnPosition, Quaternion.identity);
        

    }
}

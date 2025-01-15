using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJuego : MonoBehaviour
{
    public static ControladorJuego instance;

    public void Awake()
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
    public void guardarAvance()
    {
        SistemaGuardado.GuardarDatos(Inventario.instance, HealthBar.instance);
    }
    public void cargarAvance()
    {
        DatosGuardado datos = SistemaGuardado.CargarDatos();

        Inventario.instance.setDinero(datos.dinero);
        HealthBar.instance.setVidaActual(datos.salud);

        Vector3 posicion;
        posicion.x = datos.position[0];
        posicion.y = datos.position[1];
        posicion.z = datos.position[2];
        HealthBar.instance.transform.position = posicion;
    }
}

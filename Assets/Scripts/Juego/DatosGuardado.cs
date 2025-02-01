using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatosGuardado
{
    public int dinero;
    public float salud;
    public float[] position;

    public DatosGuardado(Inventario inventario, HealthBar healthbar) 
    {
        dinero = inventario.getDinero();
        salud = healthbar.getVidaActual();

        position = new float[3];
        position[0] = healthbar.transform.position.x;
        position[1] = healthbar.transform.position.y;
        position[2] = healthbar.transform.position.z;
    }
}
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desaparecer_enemigo : MonoBehaviour
{
    public void eliminarEnemigo()
    {
        gameObject.SetActive(false);
    }
}

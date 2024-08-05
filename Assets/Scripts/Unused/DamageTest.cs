using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{

    [SerializeField] HealthBar player;
    [SerializeField] float damage = 5.0f;
    

    void Update()
    {
     if(Input.GetMouseButtonDown(1))
        {
            player.vidaActual -= damage;
            
        }   
    }
}

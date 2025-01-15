using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorProyectil : MonoBehaviour
{
    private Rigidbody proyectilRB;

    private void Awake()
    {
        proyectilRB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        float velocidad  = 20f;
        proyectilRB.velocity = transform.forward*velocidad;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("detecta un trigger");
        if(other.gameObject.tag == "enemy")
        {
            ControladorSonido.instance.playAudio(ControladorSonido.instance.death);
        }
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectaAtaque : MonoBehaviour
{
    [SerializeField] ControladorMovimiento controladorMovimiento;
    [SerializeField] ControladorCombateDistancia controlArma;
    void Start()
    {
        HealthBar.instance.recibeDaño = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hostile")
        {
            HealthBar.instance.recibeDaño = true;
        }
    }

    public void playerDead()
    {
        HealthBar.instance.playerDied = true;
        controladorMovimiento.canMove = true;   
    }
    public void FinishDamage()
    {

        controladorMovimiento.GetComponent<Collider>().enabled = true;
        controladorMovimiento.GetComponent<Rigidbody>().isKinematic = false;
        controladorMovimiento.canMove = true;
        controladorMovimiento.estaCorriendo=false;



    }
    public void canShoot()
    {
        controlArma.shooting = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorRecibeDaño : MonoBehaviour
{
    [SerializeField] ControladorMovimiento controladorMovimiento;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hostile")
        {
            HealthBar.instance.setRecibeDaño(true);
        }
    }

    public void playerDead()
    {
        HealthBar.instance.setJugadorMuriendo(true);
        controladorMovimiento.setCanMove(true);
        controladorMovimiento.getAnim().SetBool("Muere", false);
    }
    public void FinalizarRecibirDaño()
    {

        controladorMovimiento.GetComponent<Collider>().enabled = true;
        controladorMovimiento.GetComponent<Rigidbody>().isKinematic = false;
        controladorMovimiento.setCanMove(true);
        controladorMovimiento.getAnim().SetBool("RecibeDaño", false);



    }

}

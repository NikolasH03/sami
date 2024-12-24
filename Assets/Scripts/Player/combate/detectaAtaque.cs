using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectaAtaque : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] weaponController controlArma;
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
            player.canMove = true;   
    }
    public void FinishDamage()
    {

            player.GetComponent<Collider>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.canMove = true;
        player.isSprinting=false;



    }
    public void canShoot()
    {
        controlArma.shooting = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectaAtaque : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] Player player;
    void Start()
    {
        healthBar.detectaAtaque = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hostile")
        {
            healthBar.detectaAtaque = true;
        }
    }

    public void playerDead()
    {
        healthBar.playerDied = true;
            player.canMove = true;   
    }
    public void FinishDamage()
    {

            Player.instance.GetComponent<Collider>().enabled = true;
            player.canMove = true;
        

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateMachineBehaviour
{
    private Player player;
    [SerializeField] GameObject[] enemies;
    private logicaEnemigo enemigo;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        player = animator.GetComponent<Player>();
        enemies = GameObject.FindGameObjectsWithTag("enemy");

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        
        if (player == null)
            return;

        if (player.atacandoDebil)
        {
            player.anim.Play("ataque");
            AudioManager.instance.playAudio(AudioManager.instance.attack);
            player.numero_golpesDebiles++;
            
            for (int i = 0; i < enemies.Length; i++)
            {
                enemigo = enemies[i].GetComponent<logicaEnemigo>();
                enemigo.tipoDeDaño("ligero");
            }
           
        }
        if (player.atacandoFuerte)
        {
            player.anim.Play("ataqueFuerte1");
            AudioManager.instance.playAudio(AudioManager.instance.heavyAttack);
            player.numero_golpesFuertes++;

            for (int i = 0; i < enemies.Length; i++)
            {
                enemigo = enemies[i].GetComponent<logicaEnemigo>();
                enemigo.tipoDeDaño("fuerte");
            }
            
        }
      
        if (player.anim.GetBool("blocking"))
        {
            player.anim.Play("bloqueando");
           
            
        } 
        if (player.isDashing)
        {
            player.anim.Play("dash");
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
            return;

        player.atacando = false;
        player.atacandoDebil = false;
        player.atacandoFuerte = false;
        player.isDashing = false;

    }


}

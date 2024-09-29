using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEspanol : StateMachineBehaviour
{
    private Player player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        player = animator.GetComponent<Player>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
            return;

        if (player.atacandoDebil)
        {
            player.anim.Play("ataque");
            player.numero_golpesDebiles++;
            logicaEnemigo.instance.tipoDeDaño("ligero");
        }
        if (player.atacandoFuerte)
        {
            player.anim.Play("ataqueFuerte1");
            player.numero_golpesFuertes++;
            logicaEnemigo.instance.tipoDeDaño("fuerte");
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

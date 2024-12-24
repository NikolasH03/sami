using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHeavy2 : StateMachineBehaviour
{
    private Player player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = animator.GetComponent<Player>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButton(1))
        {

            player.anim.Play("ataqueFuerte3");
            AudioManager.instance.playAudio(AudioManager.instance.heavyAttack);
            player.numero_golpesFuertes++;
        }
        else if (Input.GetMouseButton(0))
        {
            player.anim.Play("combo2");
        }

        
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.atacando = false;
        player.atacandoFuerte = false;

        
    }


}

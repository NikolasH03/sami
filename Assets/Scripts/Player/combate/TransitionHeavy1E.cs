using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHeavy1E : StateMachineBehaviour
{
    private Player player;
    AudioManager audioManager;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = animator.GetComponent<Player>();
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButton(1))
        {

            player.anim.Play("ataqueFuerte2");
            audioManager.playAudio(audioManager.heavyAttack);
            player.numero_golpesFuertes++;
        }
        else if (Input.GetMouseButton(0))
        {
            player.anim.Play("ataque");
            audioManager.playAudio(audioManager.attack);
            player.numero_golpesDebiles++;
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player.atacando = false;
        player.atacandoFuerte = false;
    }


}

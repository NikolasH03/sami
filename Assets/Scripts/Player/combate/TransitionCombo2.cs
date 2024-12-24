using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCombo2: StateMachineBehaviour
{
    private Player player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = animator.GetComponent<Player>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.numero_golpesDebiles = 0;
        player.numero_golpesFuertes = 0;
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.atacando = false;
        player.atacandoFuerte = false;
        player.atacandoDebil = false;
    }


}

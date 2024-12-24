using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition3: StateMachineBehaviour
{
    private Player player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = animator.GetComponent<Player>();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.atacando = false;
        player.atacandoDebil = false;
        player.atacandoFuerte = false;
    }


}

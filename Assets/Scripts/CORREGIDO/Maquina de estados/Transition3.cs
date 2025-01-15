using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition3: StateMachineBehaviour
{
    private ControladorCombate player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = animator.GetComponent<ControladorCombate>();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.setAtacando(false);
    }


}

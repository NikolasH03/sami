using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCombo2: StateMachineBehaviour
{
    private ControladorCombate player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = animator.GetComponent<ControladorCombate>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.numeroGolpesLigeros = 0;
        player.numeroGolpesFuertes = 0;
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.setAtacando(false);
    }


}

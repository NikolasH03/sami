using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : StateMachineBehaviour
{
    private ControladorCombate player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = animator.GetComponent<ControladorCombate>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButton(0))
        {
            player.anim.Play("ataque2");
            ControladorSonido.instance.playAudio(ControladorSonido.instance.attack);
            player.numeroGolpesLigeros++;
            player.tipoAtaque = "ligero";
        }
        else if (Input.GetMouseButton(1))
        {
            player.anim.Play("ataqueFuerte1");
            ControladorSonido.instance.playAudio(ControladorSonido.instance.heavyAttack);
            player.numeroGolpesFuertes++;
            player.tipoAtaque = "fuerte";
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player.setAtacando(false);
    }


}

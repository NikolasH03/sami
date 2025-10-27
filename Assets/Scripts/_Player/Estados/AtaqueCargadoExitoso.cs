using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class AtaqueCargadoExitoso : CombatState
{
    public AtaqueCargadoExitoso(CombatStateMachine fsm, ControladorCombate cc) : base(fsm, cc)    {   }
    public override void Enter()
    {
        combatController.tipoAtaque = "cargado";
        combatController.OrientarJugador(combatController.ultimoInputMovimiento);
        combatController.InvulneravilidadJugador();
        combatController.anim.SetTrigger("CargadoExitoso");
        combatController.setAtacando(true);
        combatController.ReproducirSonido(6, 1);
        Debug.Log("el ataque cargado fue exitoso");
    }

    public override void Exit()
    {
        combatController.setAtacando(false);
        combatController.DesactivarTodosLosTrails();
        combatController.DesactivarTodosLosCollider();
        combatController.DesactivarVentanaCombo();
        combatController.TerminarInvulnerabilidad();
    }
}
